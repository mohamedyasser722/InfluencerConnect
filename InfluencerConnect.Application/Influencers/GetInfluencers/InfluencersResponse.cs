using InfluencerConnect.Domain.Influencers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfluencerConnect.Application.Influencers.GetInfluencers;
public record InfluencersResponse(
    Guid Id,
    string FullName,
    string Bio,
    string ProfilePictureUrl,
    Gender Gender,
    int Age,
    PriceRange PriceRange,
    ICollection<SocialMediaProfileResponse> SocialMediaProfiles
);

public record SocialMediaProfileResponse(
    string PlatformName,
    string AccountUserName,
    string ProfileLink,
    string FollowersCount
);
