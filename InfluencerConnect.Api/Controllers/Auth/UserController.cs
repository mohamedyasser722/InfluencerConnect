using InfluencerConnect.Api.Controllers.Auth.Contracts;
using InfluencerConnect.Application.ApplicationUsers.Login;
using InfluencerConnect.Application.ApplicationUsers.RegisterUser;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InfluencerConnect.Api.Controllers.Auth;
[Route("auth")]
[ApiController]
public class UserController(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;

    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync(RegisterUserRequest request)
    {
        var command = new RegisterUserCommand(request.FirstName, request.LastName, request.Email, request.Password, request.UserType);

        var response = await _sender.Send(command);

        return response.IsSuccess ? Ok(response.Value) : BadRequest(response.Error);
    }
    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync(LoginUserRequest request)
    {
        var command = new LoginUserCommand(request.Email, request.Password);

        var response = await _sender.Send(command);

        return response.IsSuccess ? Ok(response.Value) : BadRequest(response.Error);
    }
}
