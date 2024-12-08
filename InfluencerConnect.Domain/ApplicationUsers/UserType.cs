using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfluencerConnect.Domain.ApplicationUsers;

/// <summary>
/// Represents the type of user in the system.
/// </summary>
/// <remarks>
/// If new user types are added, ensure that the <see cref="UserTypeExtensions.IsValidForRegistration(UserType)"/> method is updated accordingly.
/// </remarks>
public enum UserType
{
    Influencer,
    Brand,
    Admin

}
/// <summary>
/// Determines whether a <see cref="UserType"/> is valid for registration.
/// </summary>
/// <remarks>
/// Ensure this method is updated if new <see cref="UserType"/> values are added.
/// </remarks>
public static class UserTypeExtensions
{
    public static bool IsValidForRegistration(this UserType userType)
    {
        // If new UserType values are added, ensure this logic is updated.
        return userType == UserType.Influencer || userType == UserType.Brand;
    }
}
