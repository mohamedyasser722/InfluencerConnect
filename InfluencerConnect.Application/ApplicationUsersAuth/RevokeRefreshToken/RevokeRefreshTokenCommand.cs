using InfluencerConnect.Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfluencerConnect.Application.ApplicationUsersAuth.RevokeRefreshToken;
public sealed record RevokeRefreshTokenCommand(string Token, string RefreshToken) : ICommand<bool>;