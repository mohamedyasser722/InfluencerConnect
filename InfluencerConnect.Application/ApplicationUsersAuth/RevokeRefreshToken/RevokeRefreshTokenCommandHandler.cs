using InfluencerConnect.Application.Abstractions.Authentication;
using InfluencerConnect.Application.Abstractions.Messaging;
using InfluencerConnect.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfluencerConnect.Application.ApplicationUsersAuth.RevokeRefreshToken;
public sealed class RevokeRefreshTokenCommandHandler(IJwtService jwtService) : ICommandHandler<RevokeRefreshTokenCommand, bool>
{
    private readonly IJwtService _jwtService = jwtService;
    public Task<Result<bool>> Handle(RevokeRefreshTokenCommand request, CancellationToken cancellationToken)
    {
        return _jwtService.RevokeRefreshTokenAsync(request.Token, request.RefreshToken, cancellationToken);
    }
}
