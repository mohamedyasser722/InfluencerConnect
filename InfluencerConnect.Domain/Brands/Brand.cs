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
         ICollection<BrandSocialMediaProfile> brandSocialMediaProfile) : base(Guid.NewGuid())
    {
        ApplicationUserId = applicationUser.Id;
        ApplicationUser = applicationUser;
        BrandInfo = brandInfo;
        BrandSocialMediaProfile = brandSocialMediaProfile ?? [];
    }
    public string ApplicationUserId { get; set; }
    public ApplicationUser ApplicationUser { get; set; }
    public BrandInfo BrandInfo { get; set; }
    public ICollection<BrandSocialMediaProfile> BrandSocialMediaProfile { get; set; } = [];

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
