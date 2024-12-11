using InfluencerConnect.Domain.Abstractions;
using InfluencerConnect.Domain.ApplicationUsers;
using InfluencerConnect.Domain.Brands.Events;
using InfluencerConnect.Domain.Influencers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfluencerConnect.Domain.Brands;

public sealed class Brand : Entity
{
    private Brand() { }

    private Brand(ApplicationUser applicationUser)
    {
        Id = applicationUser.Id;
        ApplicationUser = applicationUser ?? throw new ArgumentNullException(nameof(applicationUser));
        // Leave other properties uninitialized for now
    }

    public Guid Id { get; private set; }
    public ApplicationUser ApplicationUser { get; private set; }
    public BrandInfo? BrandInfo { get; private set; }  // Nullable
    public ICollection<BrandSocialMediaProfile>? BrandSocialMediaProfile { get; private set; }  // Nullable

    public static Brand Create(ApplicationUser applicationUser)
    {
        var brand = new Brand(applicationUser);
        brand.RaiseDomainEvent(new BrandCreatedDomainEvent(brand.Id));
        return brand;
    }
}


