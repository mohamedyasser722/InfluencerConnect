using InfluencerConnect.Application.Abstractions.Messaging;
using InfluencerConnect.Domain.Abstractions;
using InfluencerConnect.Domain.ApplicationUsers;
using InfluencerConnect.Domain.Brands;
using InfluencerConnect.Domain.Influencers;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfluencerConnect.Application.ApplicationUsersAuth.RegisterUser;
public class RegisterUserCommandHandler(UserManager<ApplicationUser> userManager) : ICommandHandler<RegisterUserCommand, Guid>
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    public async Task<Result<Guid>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        if (!UserTypeExtensions.IsValidForRegistration(request.UserType))
            return Result.Failure<Guid>(ApplicationUserErrors.InvalidUserType);

        var newUser = new ApplicationUser
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            UserName = request.Email.Split('@')[0],
            Email = request.Email,
            UserType = request.UserType,
            IsAccepted = false
        };

        // Create Influencer or Brand object, but without detailed data
        if (request.UserType == UserType.Influencer)
            newUser.Influencer = Influencer.Create(newUser);
        else
            newUser.Brand = Brand.Create(newUser);

        var result = await _userManager.CreateAsync(newUser, request.Password);
        if (!result.Succeeded)
        {
            var error = result.Errors.FirstOrDefault();
            return Result.Failure<Guid>(new Error(error.Code, error.Description));
        }

        return Result.Success(newUser.Id);
    }
}
