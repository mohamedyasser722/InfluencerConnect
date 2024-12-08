using InfluencerConnect.Application.Abstractions.Authentication;
using InfluencerConnect.Application.Abstractions.Messaging;
using InfluencerConnect.Domain.Abstractions;
using InfluencerConnect.Domain.ApplicationUsers;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfluencerConnect.Application.ApplicationUsersAuth.Login;
public sealed class LoginUserCommandHandler(IJwtService jwtService) : ICommandHandler<LoginUserCommand, AuthResponse>
{
    private readonly IJwtService _jwtService = jwtService;

    public Task<Result<AuthResponse>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        return _jwtService.GetTokenAsync(request.Email, request.Password, cancellationToken);
    }
}
