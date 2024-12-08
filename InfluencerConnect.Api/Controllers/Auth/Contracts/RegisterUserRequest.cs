using InfluencerConnect.Domain.ApplicationUsers;

namespace InfluencerConnect.Api.Controllers.Auth.Contracts;

public sealed record RegisterUserRequest
(
    string FirstName,
    string LastName,
    string Email,
    string Password,
    UserType UserType
);