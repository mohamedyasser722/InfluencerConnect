
using InfluencerConnect.Application.Abstractions.Authentication;
using InfluencerConnect.Application.Abstractions.Messaging;

namespace InfluencerConnect.Application.ApplicationUsers.Login;

public sealed record LoginUserCommand(string Email, string Password) : ICommand<AuthResponse>;