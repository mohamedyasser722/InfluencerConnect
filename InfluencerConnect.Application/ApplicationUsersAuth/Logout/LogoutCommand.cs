using InfluencerConnect.Application.Abstractions.Messaging;
using InfluencerConnect.Domain.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfluencerConnect.Application.ApplicationUsersAuth.Logout;
public sealed record LogoutCommand(string Token, string RefreshToken) : ICommand<string>;