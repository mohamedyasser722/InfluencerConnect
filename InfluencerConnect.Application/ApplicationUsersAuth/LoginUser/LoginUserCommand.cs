
using InfluencerConnect.Application.Abstractions.Authentication;
using InfluencerConnect.Application.Abstractions.Messaging;

namespace InfluencerConnect.Application.ApplicationUsersAuth.Login;

public sealed record LoginUserCommand(string Email, string Password) : ICommand<AuthResponse>;