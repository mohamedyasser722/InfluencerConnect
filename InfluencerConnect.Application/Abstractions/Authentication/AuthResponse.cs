using InfluencerConnect.Domain.ApplicationUsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfluencerConnect.Application.Abstractions.Authentication;
public sealed record AuthResponse
(
    Guid Id,
    string? Email,
    string FirstName,
    string LastName,
    string UserType,
    string Token,
    int ExpiresIn,
    string RefreshToken,
    DateTime RefreshTokenExpiration
);
