namespace InfluencerConnect.Api.Controllers.Auth.Contracts;

public sealed record LoginUserRequest
(
    string Email,
    string Password
);
