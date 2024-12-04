using InfluencerConnect.Domain.Abstractions;
using InfluencerConnect.Domain.ApplicationUsers;
using InfluencerConnect.Domain.Influencers.Events;
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
        ApplicationUser applicationUser,
        BirthDate birthDate,
        Gender gender,
        PriceRange priceRange,
        string bio,
        string profilePictureUrl,
        ICollection<InfluencerSocialMediaProfile> influencerSocialMediaProfiles
    ) : base(Guid.NewGuid())
    {
        ApplicationUserId = applicationUser.Id;
        ApplicationUser = applicationUser;
        BirthDate = birthDate;
        Gender = gender;
        PriceRange = priceRange;
        Bio = bio;
        ProfilePictureUrl = profilePictureUrl;
        InfluencerSocialMediaProfiles = influencerSocialMediaProfiles ?? [];
    }

    public string ApplicationUserId { get; private set; }
    public ApplicationUser ApplicationUser { get; private set; }
    public BirthDate BirthDate { get; private set; }
    public Gender Gender { get; private set; }
    public PriceRange PriceRange { get; private set; }
    public string Bio { get; private set; }
    public string ProfilePictureUrl { get; private set; }
    public ICollection<InfluencerSocialMediaProfile> InfluencerSocialMediaProfiles { get; private set; } = [];

    public static Influencer Create(
        ApplicationUser applicationUser,
        BirthDate birthDate,
        Gender gender,
        PriceRange priceRange,
        string bio,
        string profilePictureUrl,
        ICollection<InfluencerSocialMediaProfile> socialMediaProfiles
    )
    {
        var influencer = new Influencer(
            applicationUser,
            birthDate,
            gender,
            priceRange,
            bio,
            profilePictureUrl,
            socialMediaProfiles
        );

        influencer.RaiseDomainEvent(new InfluencerCreatedDomainEvent(influencer.Id));
        return influencer;
    }
}
