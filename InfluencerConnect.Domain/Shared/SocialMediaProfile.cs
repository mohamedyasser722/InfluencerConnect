namespace InfluencerConnect.Domain.Shared;
public sealed record SocialMediaProfile
(
    string PlatformName,
    string AccountUserName,
    string ProfileLink,
    int FollowersCount
)
{
    public string FormattedFollowersCount => FormatFollowersCount(FollowersCount);

    private static string FormatFollowersCount(int count)
    {
        if (count >= 1_000_000)
            return $"{count / 1_000_000.0:0.#}M"; // 1.2M
        if (count >= 1_000)
            return $"{count / 1_000.0:0.#}K"; // 10.5K
        return count.ToString(); // 999
    }
}
