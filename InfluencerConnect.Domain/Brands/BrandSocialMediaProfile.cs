using InfluencerConnect.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfluencerConnect.Domain.Brands;
public class BrandSocialMediaProfile : Entity
{
    public Guid Id { get; private set; }
    public Guid BrandId { get; init; }
    public string PlatformName { get; init; }
    public string AccountUserName { get; init; }
    public string ProfileLink { get; init; }
    public string FollowersCount => FormatFollowersCount(FollowersCountRaw);

    private int FollowersCountRaw { get; init; }

    private BrandSocialMediaProfile() { }
    public BrandSocialMediaProfile(
        Guid influencerId,
        string platformName,
        string accountUserName,
        string profileLink,
        int followersCount)
    {
        if (BrandId == Guid.Empty)
            throw new ArgumentException("BrandId cannot be empty.", nameof(BrandId));
        if (string.IsNullOrWhiteSpace(platformName))
            throw new ArgumentException("PlatformName cannot be empty.", nameof(platformName));
        if (string.IsNullOrWhiteSpace(accountUserName))
            throw new ArgumentException("AccountUserName cannot be empty.", nameof(accountUserName));
        if (string.IsNullOrWhiteSpace(profileLink))
            throw new ArgumentException("ProfileLink cannot be empty.", nameof(profileLink));
        if (followersCount < 0)
            throw new ArgumentOutOfRangeException(nameof(followersCount), "Followers count cannot be negative.");

        Id = Guid.NewGuid();
        BrandId = influencerId;
        PlatformName = platformName;
        AccountUserName = accountUserName;
        ProfileLink = profileLink;
        FollowersCountRaw = followersCount;
    }

    private static string FormatFollowersCount(int count)
    {
        if (count >= 1_000_000)
            return $"{count / 1_000_000.0:0.#}M";
        if (count >= 1_000)
            return $"{count / 1_000.0:0.#}K";
        return count.ToString();
    }
}
