using InfluencerConnect.Application.Abstractions.Authentication;
using InfluencerConnect.Domain.Abstractions;
using InfluencerConnect.Domain.ApplicationUsers;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
namespace InfluencerConnect.Infrastructure.Authentication;

public class JwtService(
    UserManager<ApplicationUser> userManager,
    IJwtProvider jwtProvider,
    IOptions<RefreshTokenOptions> options) : IJwtService
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly IJwtProvider _jwtProvider = jwtProvider;
    private readonly RefreshTokenOptions _options = options.Value;

    public async Task<Result<AuthResponse>> GetTokenAsync(string email, string password, CancellationToken cancellationToken = default)
    {
        // Find user by email
        var user = await _userManager.FindByEmailAsync(email);

        if (user == null)
            return Result.Failure<AuthResponse>(ApplicationUserErrors.InvalidCredentials);

        // Validate password
        bool isValidPassword = await _userManager.CheckPasswordAsync(user, password);

        if (!isValidPassword)
            return Result.Failure<AuthResponse>(ApplicationUserErrors.InvalidCredentials);

      
        return await GenerateAuthResponseAsync(user, cancellationToken);

    }


    public async Task<Result<AuthResponse>> GetRefreshTokenAsync(string token, string refreshToken, CancellationToken cancellationToken = default)
    {
        var userId = _jwtProvider.ValidateToken(token);

        if(userId == null)
            return Result.Failure<AuthResponse>(ApplicationUserErrors.InvalidToken);
        // Find the user by ID
        var user = await _userManager.FindByIdAsync(userId);
        if (user is null)
            return Result.Failure<AuthResponse>(ApplicationUserErrors.UserNotFound);

        // Validate the refresh token
        RefreshToken userRefreshToken = user.RefreshTokens.SingleOrDefault(rt => rt.Token == refreshToken && rt.IsActive);
        if (userRefreshToken is null)
            return Result.Failure<AuthResponse>(ApplicationUserErrors.InvalidRefreshToken);

        // Invalidate the old refresh token
        userRefreshToken.RevokedOn = DateTime.UtcNow; // Mark it as revoked

        // Generate a new JWT token and refresh token
       return await GenerateAuthResponseAsync(user, cancellationToken);

    }


    private async Task<Result<AuthResponse>> GenerateAuthResponseAsync(ApplicationUser user, CancellationToken cancellationToken)
    {

        var (token, expiresIn) = _jwtProvider.GenerateToken(user);

        var (refreshToken, refreshTokenExpiry) = GenerateRefreshToken(_options);

        // Persist the refresh token
        var newRefreshToken = new RefreshToken
        {
            Token = refreshToken,
            ExpiresOn = refreshTokenExpiry
        };

        user.RefreshTokens.Add(newRefreshToken);

        // Update the user with the new refresh token
        var result = await _userManager.UpdateAsync(user);

        if (!result.Succeeded)
        {
            return Result.Failure<AuthResponse>(ApplicationUserErrors.FailedToUpdateUser);
        }

        // Return Auth Response
        return Result.Success(new AuthResponse(
            user.Id,
            user.Email,
            user.FirstName,
            user.LastName,
            user.UserType.ToString(),
            token,
            expiresIn,
            refreshToken,
            refreshTokenExpiry
        ));
    }
    private static (string, DateTime) GenerateRefreshToken(RefreshTokenOptions refreshTokenOptions)
    {
        var refreshToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
        var refreshTokenExpiry = DateTime.UtcNow.AddDays(refreshTokenOptions.RefreshTokenExpiryInDays);

        return (refreshToken, refreshTokenExpiry);
    }
}
