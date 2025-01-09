namespace InfluencerConnect.Api.Controllers.Auth.Contracts;

public sealed record LogoutRequest(string Token, string RefreshToken);