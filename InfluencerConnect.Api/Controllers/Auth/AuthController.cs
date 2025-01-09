using InfluencerConnect.Api.Controllers.Auth.Contracts;
using InfluencerConnect.Application.ApplicationUsersAuth.Login;
using InfluencerConnect.Application.ApplicationUsersAuth.Logout;
using InfluencerConnect.Application.ApplicationUsersAuth.RefreshToken;
using InfluencerConnect.Application.ApplicationUsersAuth.RegisterUser;
using InfluencerConnect.Application.ApplicationUsersAuth.RevokeRefreshToken;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InfluencerConnect.Api.Controllers.Auth;
[Route("auth")]
[ApiController]
public class AuthController(ISender sender) : ControllerBase
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
    [HttpPost("logout")]
    public async Task<IActionResult> LogoutAsync(LogoutRequest request)
    {
        var command = new LogoutCommand(request.Token, request.RefreshToken);
        var response = await _sender.Send(command);

        return response.IsSuccess ? Ok(response.Value) : BadRequest(response.Error);
    }


    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshAsync(RefreshTokenRequest request)
    {
        var command = new GetRefreshTokenCommand(request.Token, request.RefreshToken);

        var response = await _sender.Send(command);

        return response.IsSuccess ? Ok(response.Value) : BadRequest(response.Error);
    }
    [HttpPost("revoke-refresh-token")]
    public async Task<IActionResult> RevokeRefreshTokenAsync(RefreshTokenRequest request)
    {
        var command = new RevokeRefreshTokenCommand(request.Token, request.RefreshToken);

        var response = await _sender.Send(command);

        return response.IsSuccess ? Ok(response.Value) : BadRequest(response.Error);
    }

}
