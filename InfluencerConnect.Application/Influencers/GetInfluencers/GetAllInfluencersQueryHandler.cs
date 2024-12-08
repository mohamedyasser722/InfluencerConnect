using InfluencerConnect.Application.Abstractions.Messaging;
using InfluencerConnect.Domain.Abstractions;
using InfluencerConnect.Domain.Influencers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfluencerConnect.Application.Influencers.GetInfluencers;
public class GetAllInfluencersQueryHandler : IQueryHandler<GetAllInfluencersQuery, List<InfluencersResponse>>
{
    private readonly IInfluencerRepository _influencerRepository;

    public GetAllInfluencersQueryHandler(IInfluencerRepository influencerRepository)
    {
        _influencerRepository = influencerRepository;
    }

    public async Task<Result<List<InfluencersResponse>>> Handle(GetAllInfluencersQuery request, CancellationToken cancellationToken)
    {
        var influencers = await _influencerRepository.GetAllWithDetailsAsync(cancellationToken);

        var response = influencers.Select(i => MapToInfluencersResponse(i)).ToList();

        return Result.Success(response);
    }
    private InfluencersResponse MapToInfluencersResponse(Influencer influencer)
    {
        return new InfluencersResponse(
            Id: influencer.Id,
            FullName: $"{influencer.ApplicationUser.FirstName} {influencer.ApplicationUser.LastName}",
            Bio: influencer.Bio,
            ProfilePictureUrl: influencer.ProfilePictureUrl,
            Gender: influencer.Gender,
            Age: influencer.BirthDate.Age,
            PriceRange: influencer.PriceRange,
            SocialMediaProfiles: influencer.InfluencerSocialMediaProfiles.Select(s => new SocialMediaProfileResponse(
                PlatformName: s.PlatformName,
                AccountUserName: s.AccountUserName,
                ProfileLink: s.ProfileLink,
                FollowersCount: s.FollowersCount
            )).ToList()
        );
    }

}
