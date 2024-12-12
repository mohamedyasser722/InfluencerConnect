using InfluencerConnect.Application.Abstractions.Authentication;
using InfluencerConnect.Application.Abstractions.Messaging;
using InfluencerConnect.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfluencerConnect.Application.ApplicationUsersAuth.RefreshToken;
public sealed class GetRefreshTokenCommandHandler(IJwtService jwtService) : ICommandHandler<GetRefreshTokenCommand, AuthResponse>
{
    private readonly IJwtService _jwtService = jwtService;

    public async Task<Result<AuthResponse>> Handle(GetRefreshTokenCommand request, CancellationToken cancellationToken)
    {
        return await _jwtService.GetRefreshTokenAsync(request.Token, request.RefreshToken, cancellationToken);
    }
}
