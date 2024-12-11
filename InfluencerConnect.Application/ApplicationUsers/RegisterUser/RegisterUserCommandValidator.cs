using FluentValidation;
using InfluencerConnect.Domain.ApplicationUsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfluencerConnect.Application.ApplicationUsers.RegisterUser;
public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required.")
            .MaximumLength(50).WithMessage("First name must not exceed 50 characters.");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required.")
            .MaximumLength(50).WithMessage("Last name must not exceed 50 characters.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("A valid email address is required.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.");
        //.MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
        //.Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
        //.Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter.")
        //.Matches(@"[0-9]").WithMessage("Password must contain at least one digit.")
        //.Matches(@"[\W]").WithMessage("Password must contain at least one special character.");

        RuleFor(x => x.UserType)
            .Must(UserTypeExtensions.IsValidForRegistration).WithMessage("Invalid user type.");
    }
}
