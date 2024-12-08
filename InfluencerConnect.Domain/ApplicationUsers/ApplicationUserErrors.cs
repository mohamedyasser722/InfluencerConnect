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

}
