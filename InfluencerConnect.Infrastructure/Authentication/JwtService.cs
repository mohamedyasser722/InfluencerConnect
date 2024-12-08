using InfluencerConnect.Application.Abstractions.Authentication;
using InfluencerConnect.Domain.Abstractions;
using InfluencerConnect.Domain.ApplicationUsers;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfluencerConnect.Infrastructure.Authentication;
public class JwtService(UserManager<ApplicationUser> userManager, IJwtProvider jwtProvider) : IJwtService
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly IJwtProvider _jwtProvider = jwtProvider;
    public async Task<Result<AuthResponse>> GetTokenAsync(string email, string password, CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if(user == null) return Result.Failure<AuthResponse>(ApplicationUserErrors.InvalidCredentials);

        bool isValidPassword = await _userManager.CheckPasswordAsync(user, password);

        if(!isValidPassword) return Result.Failure<AuthResponse>(ApplicationUserErrors.InvalidCredentials);

        // generate Jwt Token

        var (token, expiresIn) = _jwtProvider.GenerateToken(user);

        return new AuthResponse(user.Id, user.Email, user.FirstName, user.LastName, token, expiresIn);

         
    }
}
