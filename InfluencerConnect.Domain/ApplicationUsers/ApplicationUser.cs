using InfluencerConnect.Domain.Brands;
using InfluencerConnect.Domain.Influencers;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfluencerConnect.Domain.ApplicationUsers;
public sealed class ApplicationUser : IdentityUser<Guid>
{
    public ApplicationUser()
    {
        Id = Guid.NewGuid();
    }
    public ApplicationUser(string firstName, string lastName, UserType userType, bool isAccepted)
    {
        Id = Guid.NewGuid();
        FirstName = firstName;
        LastName = lastName;
        UserType = userType;
        IsAccepted = false;
    }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public UserType UserType { get; set; }
    public bool IsAccepted { get; set; }

    public Influencer? Influencer { get; set; }
    public Brand? Brand { get; set; }
    // list of refreshtoken TODO
}
