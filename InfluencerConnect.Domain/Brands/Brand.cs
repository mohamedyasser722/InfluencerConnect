using InfluencerConnect.Domain.Abstractions;
using InfluencerConnect.Domain.ApplicationUsers;
using InfluencerConnect.Domain.Brands.Events;
using InfluencerConnect.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfluencerConnect.Domain.Brands;
public sealed class Brand : Entity
{
    private Brand(){}
    private Brand(Guid id,
        ApplicationUser applicationUser,
       BrandInfo brandInfo,
        List<SocialMediaProfile> socialMediaProfile) : base(id)
    {
        ApplicationUserId = applicationUser.Id;
        ApplicationUser = applicationUser;
        BrandInfo = brandInfo;
        SocialMediaProfile = socialMediaProfile ?? new List<SocialMediaProfile>();
    }
    public string ApplicationUserId { get; set; }
    public ApplicationUser ApplicationUser { get; set; }
    public BrandInfo BrandInfo { get; set; }
    public List<SocialMediaProfile> SocialMediaProfile { get; set; } = [];

    public static Brand Create(
        ApplicationUser applicationUser,
        BrandInfo brandInfo,
        List<SocialMediaProfile> socialMediaProfile
        )
    {
        var brand = new Brand(
            Guid.NewGuid(),
            applicationUser,
            brandInfo,
            socialMediaProfile
            );

        brand.RaiseDomainEvent(new BrandCreatedDomainEvent(brand.Id));

        return brand;
    }

}
