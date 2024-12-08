using InfluencerConnect.Application.Abstractions.Messaging;
using InfluencerConnect.Domain.Abstractions;
using InfluencerConnect.Domain.ApplicationUsers;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfluencerConnect.Application.ApplicationUsersAuth.RegisterUser;
public class RegisterUserCommandHandler(UserManager<ApplicationUser> userManager) : ICommandHandler<RegisterUserCommand, string>
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    public async Task<Result<string>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var newUser = new ApplicationUser
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            UserName = request.Email.Split('@')[0],
            Email = request.Email,
            UserType = request.UserType,
            IsAccepted = false
        };

        var result = await _userManager.CreateAsync(newUser, request.Password);
        if (!result.Succeeded)
        {
            var error = result.Errors.FirstOrDefault();
            return Result.Failure<string>(new Error(error.Code, error.Description));
        }

        return Result.Success(newUser.Id);
    }
}
