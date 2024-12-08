using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfluencerConnect.Domain.ApplicationUsers;
public sealed class ApplicationUser : IdentityUser
{
    public ApplicationUser()
    {
    }
    public ApplicationUser(string firstName, string lastName, UserType userType, bool isAccepted)
    {
        FirstName = firstName;
        LastName = lastName;
        UserType = userType;
        IsAccepted = false;
    }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public UserType UserType { get; set; }
    public bool IsAccepted { get; set; }
    // list of refreshtoken TODO
}
