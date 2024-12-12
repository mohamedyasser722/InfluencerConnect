using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfluencerConnect.Application.ApplicationUsersAuth.RefreshToken;
public class GetRefreshTokenCommandValidator : AbstractValidator<GetRefreshTokenCommand>
{
    public GetRefreshTokenCommandValidator()
    {
        RuleFor(x => x.RefreshToken)
            .NotEmpty();
        
        RuleFor(x => x.Token)
            .NotEmpty();
    }
}
