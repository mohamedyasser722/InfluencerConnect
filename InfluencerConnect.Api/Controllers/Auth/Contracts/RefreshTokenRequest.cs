namespace InfluencerConnect.Api.Controllers.Auth.Contracts;

public sealed record RefreshTokenRequest
(string Token, string RefreshToken);
