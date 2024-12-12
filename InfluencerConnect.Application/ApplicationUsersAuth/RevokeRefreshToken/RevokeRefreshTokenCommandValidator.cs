using FluentValidation;


namespace InfluencerConnect.Application.ApplicationUsersAuth.RevokeRefreshToken;
public class RevokeRefreshTokenCommandValidator : AbstractValidator<RevokeRefreshTokenCommand>
{
    public RevokeRefreshTokenCommandValidator()
    {
        RuleFor(x => x.RefreshToken)
            .NotEmpty();

        RuleFor(x => x.Token)
            .NotEmpty();
    }
}
