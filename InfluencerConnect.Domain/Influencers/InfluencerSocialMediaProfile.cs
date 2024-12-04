using InfluencerConnect.Domain.Abstractions;

namespace InfluencerConnect.Domain.Influencers;

public sealed class InfluencerSocialMediaProfile : Entity
{
    public Guid InfluencerId { get; init; }
    public string PlatformName { get; init; }
    public string AccountUserName { get; init; }
    public string ProfileLink { get; init; }
    public string FollowersCount => FormatFollowersCount(FollowersCountRaw);

    private int FollowersCountRaw { get; init; }

    private InfluencerSocialMediaProfile() { }
    public InfluencerSocialMediaProfile(
        Guid influencerId,
        string platformName,
        string accountUserName,
        string profileLink,
        int followersCount) : base(Guid.NewGuid())
    {
        if (InfluencerId == Guid.Empty)
            throw new ArgumentException("InfluencerId cannot be empty.", nameof(InfluencerId));
        if (string.IsNullOrWhiteSpace(platformName))
            throw new ArgumentException("PlatformName cannot be empty.", nameof(platformName));
        if (string.IsNullOrWhiteSpace(accountUserName))
            throw new ArgumentException("AccountUserName cannot be empty.", nameof(accountUserName));
        if (string.IsNullOrWhiteSpace(profileLink))
            throw new ArgumentException("ProfileLink cannot be empty.", nameof(profileLink));
        if (followersCount < 0)
            throw new ArgumentOutOfRangeException(nameof(followersCount), "Followers count cannot be negative.");

        InfluencerId = influencerId;
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
