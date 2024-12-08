using InfluencerConnect.Domain.ApplicationUsers;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace InfluencerConnect.Infrastructure.Authentication;
public class JwtProvider : IJwtProvider
{
    public (string token, int expiresIn) GenerateToken(ApplicationUser user)
    {
        Claim[] claims = [
                    
                new(JwtRegisteredClaimNames.Sub, user.Id),
                new(JwtRegisteredClaimNames.Email, user.Email!),
                new(JwtRegisteredClaimNames.GivenName, user.FirstName),
                new(JwtRegisteredClaimNames.FamilyName, user.LastName), 
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),  
                new("role", user.UserType.ToString())
            ];
        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("D34D070C939D4A8D8F3AD1584EF3FF61"));

        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: "InfluencerConnect",
            audience: "InfluencerConnect Users",
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(30),
            signingCredentials: signingCredentials
        );

        return (token: new JwtSecurityTokenHandler().WriteToken(token), expiresIn: 30);
    }
}
