using InfluencerConnect.Application.Abstractions.Authentication;
using InfluencerConnect.Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfluencerConnect.Application.ApplicationUsersAuth.RefreshToken;
public sealed record GetRefreshTokenCommand(string Token, string RefreshToken) : ICommand<AuthResponse>;
