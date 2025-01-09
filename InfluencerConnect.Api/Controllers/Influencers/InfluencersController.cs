using InfluencerConnect.Application.Influencers.GetInfluencers;
using InfluencerConnect.Domain.ApplicationUsers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InfluencerConnect.Api.Controllers.Influencers;
[Route("api/influencers")]
[ApiController]
[Authorize(Roles = "Influencer")]
public class InfluencersController(ISender sender): ControllerBase
{
    private readonly ISender _sender = sender;

    [HttpGet]
    public async Task<IActionResult> GetAllInfluencers()
    {
        //var response = await _sender.Send(new GetAllInfluencersQuery());
        return  Ok("test");
    }
}
