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

    private Brand(
        ApplicationUser applicationUser,
        BrandInfo brandInfo,
        ICollection<BrandSocialMediaProfile> brandSocialMediaProfile
    ) 
    {
        Id = applicationUser.Id;
        ApplicationUser = applicationUser ?? throw new ArgumentNullException(nameof(applicationUser));
        BrandInfo = brandInfo;
        BrandSocialMediaProfile = brandSocialMediaProfile ?? new List<BrandSocialMediaProfile>();
    }

    public Guid Id { get; private set; }
    public ApplicationUser ApplicationUser { get; private set; }
    public BrandInfo BrandInfo { get; private set; }
    public ICollection<BrandSocialMediaProfile> BrandSocialMediaProfile { get; private set; }

    public static Brand Create(
        ApplicationUser applicationUser,
        BrandInfo brandInfo,
        ICollection<BrandSocialMediaProfile> brandSocialMediaProfile
    )
    {
        var brand = new Brand(
            applicationUser,
            brandInfo,
            brandSocialMediaProfile
        );

        brand.RaiseDomainEvent(new BrandCreatedDomainEvent(brand.Id));
        return brand;
    }
}

