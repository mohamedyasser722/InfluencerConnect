using InfluencerConnect.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfluencerConnect.Domain.ApplicationUsers;
public static class ApplicationUserErrors
{
    public static readonly Error EmailExists  = new("Email_Already_Exists", "Email already exists");
    public static readonly Error InvalidUserType = new ("Invalid_User_Type", "User type is not valid for registration");
    public static readonly Error InvalidCredentials = new("invalid_credentials", "Invalid email or password");
    public static readonly Error FailedToUpdateUser = new("failed_to_update_user", "Failed to update user");
    public static readonly Error InvalidToken = new("invalid_token", "Invalid token");
    public static readonly Error InvalidRefreshToken = new("invalid_refresh_token", "Invalid refresh token");
    public static readonly Error ExpiredRefreshToken = new("expired_refresh_token", "Refresh token has expired");
    public static readonly Error UserNotFound = new("user_not_found", "User not found");
}
