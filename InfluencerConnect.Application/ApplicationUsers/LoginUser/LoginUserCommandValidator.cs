using FluentValidation;
using InfluencerConnect.Application.ApplicationUsers.Login;

namespace InfluencerConnect.Application.ApplicationUsers.Login;

public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
{
    public LoginUserCommandValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Email is not valid");
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required");
    }
}
