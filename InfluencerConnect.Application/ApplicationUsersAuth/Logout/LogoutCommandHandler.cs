using InfluencerConnect.Application.Abstractions.Authentication;
using InfluencerConnect.Application.Abstractions.Messaging;
using InfluencerConnect.Domain.Abstractions;
using InfluencerConnect.Domain.ApplicationUsers;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace InfluencerConnect.Application.ApplicationUsersAuth.Logout;

public class LogoutCommandHandler(
    UserManager<ApplicationUser> userManager,
    IJwtProvider jwtProvider,
    ITokenBlacklistService tokenBlacklistService) : ICommandHandler<LogoutCommand, string>
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly IJwtProvider _jwtProvider = jwtProvider;
    private readonly ITokenBlacklistService _tokenBlacklistService = tokenBlacklistService;
     
    public async Task<Result<string>> Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        // Validate the JWT token
        var userId = _jwtProvider.ValidateToken(request.Token);
        if (userId is null)
            return Result.Failure<string>(ApplicationUserErrors.InvalidToken);

        // Find the user by ID
        var user = await _userManager.FindByIdAsync(userId);
        if (user is null)
            return Result.Failure<string>(ApplicationUserErrors.UserNotFound);

        // Revoke the refresh token
        var refreshToken = user.RefreshTokens.SingleOrDefault(rt => rt.Token == request.RefreshToken);
        if (refreshToken is null || !refreshToken.IsActive)
            return Result.Failure<string>(ApplicationUserErrors.InvalidRefreshToken);

        refreshToken.RevokedOn = DateTime.UtcNow;

        // Blacklist the JWT
        await _tokenBlacklistService.BlacklistTokenAsync(request.Token, refreshToken.ExpiresOn);

        // Save changes
        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded)
            return Result.Failure<string>(ApplicationUserErrors.FailedUpdateRefreshTokenState);

        return Result.Success("User logged out successfully.");
    }
}