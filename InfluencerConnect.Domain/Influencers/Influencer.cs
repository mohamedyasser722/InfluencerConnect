using InfluencerConnect.Domain.Abstractions;
using InfluencerConnect.Domain.ApplicationUsers;
using InfluencerConnect.Domain.Influencers.Events;
using InfluencerConnect.Domain.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfluencerConnect.Domain.Influencers;
public sealed class Influencer : Entity
{
    private Influencer() { }
    private Influencer(
        Guid id,
        ApplicationUser applicationUser,
        PriceRange priceRange,
        Bio bio,
        ProfilePictureUrl profilePictureUrl,
        List<SocialMediaProfile> socialMediaProfiles
        ) : base(id)
    {
        ApplicationUserId = applicationUser.Id;
        ApplicationUser = applicationUser;
        PriceRange = priceRange;
        Bio = bio;
        ProfilePictureUrl = profilePictureUrl;
        SocialMediaProfiles = socialMediaProfiles ?? new List<SocialMediaProfile>();

    }

    public string ApplicationUserId { get; set; }  
    public ApplicationUser ApplicationUser { get; set; }
    public PriceRange PriceRange { get; set; }
    public Bio Bio { get; set; }
    public ProfilePictureUrl ProfilePictureUrl { get; set; }
    public List<SocialMediaProfile> SocialMediaProfiles { get; set; } = [];



    public static Influencer Create(
        ApplicationUser applicationUser,
        PriceRange priceRange,
        Bio bio,
        ProfilePictureUrl profilePictureUrl,
        List<SocialMediaProfile> socialMediaProfiles
        )
    {
        var influencer = new Influencer(
            Guid.NewGuid(),
            applicationUser,
            priceRange,
            bio,
            profilePictureUrl,
            socialMediaProfiles
            );
        influencer.RaiseDomainEvent(new InfluencerCreatedDomainEvent(influencer.Id));
        return influencer;
    }

}
