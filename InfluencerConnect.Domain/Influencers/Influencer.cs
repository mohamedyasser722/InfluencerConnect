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

    private Influencer(ApplicationUser applicationUser)
    {
        Id = applicationUser.Id;
        ApplicationUser = applicationUser ?? throw new ArgumentNullException(nameof(applicationUser));
        // Leave other properties uninitialized for now
    }

    public Guid Id { get; private set; }
    public ApplicationUser ApplicationUser { get; private set; }
    public BirthDate? BirthDate { get; private set; }  // Nullable
    public Gender? Gender { get; private set; } // Nullable
    public PriceRange? PriceRange { get; private set; } // Nullable
    public string? Bio { get; private set; }  // Nullable
    public string? ProfilePictureUrl { get; private set; }  // Nullable
    public ICollection<InfluencerSocialMediaProfile>? InfluencerSocialMediaProfiles { get; private set; }  // Nullable

    public static Influencer Create(ApplicationUser applicationUser)
    {
        var influencer = new Influencer(applicationUser);
        influencer.RaiseDomainEvent(new InfluencerCreatedDomainEvent(influencer.Id));
        return influencer;
    }
}

