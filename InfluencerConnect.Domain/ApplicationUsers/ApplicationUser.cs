using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfluencerConnect.Domain.ApplicationUsers;
public sealed class ApplicationUser : IdentityUser
{
    public Name Name { get; set; }
    public UserType UserType { get; set; }
    public BirthDate BirthDate { get; set; }
    public IsAccepted IsAccepted { get; set; }
    // list of refreshtoken TODO
}
