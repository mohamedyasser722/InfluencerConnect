using InfluencerConnect.Application.Influencers.GetInfluencers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InfluencerConnect.Api.Controllers.Influencers;
[Route("api/influencers")]
[ApiController]
public class InfluencersController(ISender sender): ControllerBase
{
    private readonly ISender _sender = sender;

    [HttpGet]
    public async Task<IActionResult> GetAllInfluencers()
    { 
        var response = await _sender.Send(new GetAllInfluencersQuery());
        return Ok(response);
    }
}
