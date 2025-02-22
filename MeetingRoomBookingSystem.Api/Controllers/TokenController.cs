using MeetingRoomBookingSystem.Core.Helpers;
using MeetingRoomBookingSystem.Core.IServices;
using MeetingRoomBookingSystem.Core.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MeetingRoomBookingSystem.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TokenController : ControllerBase
{
    private readonly ITokenService _service;
    private readonly IUserService _userService;

    public TokenController(ITokenService service, IUserService userService)
    {
        _service = service;
        _userService = userService;
    }

    [HttpPost("access")]
    public async Task<ActionResult> GetAccessTokenAsync(TokenRequest request)
    {
        var token = await _service.GenerateAccessTokenAsync(request.UserId);
        return Ok(token);
    }

    [HttpPost("refresh")]
    public async Task<ActionResult> GetNewAccessTokenAsync(TokenRequest request)
    {
        var refreshToken = await _service.GetRefreshTokenAsync(request.UserId);
        if (request.RefreshToken != refreshToken) throw new Exception();

        await _service.CheckRefreshTokenExpired(request.UserId);
        await _service.UpdateLastRequestTimeAsync(request.UserId);

        var accessToken = await _service.GenerateAccessTokenAsync(request.UserId);
        return Ok(accessToken);
    }

    [HttpPost("info")]
    [Authorize]
    public async Task<ActionResult> GetInfoFromTokenAsync()
    {
        var userId = JwtHelper.GetUserIdFromToken(HttpContext);
        if (userId == null)
        {
            return new JsonResult(new
            {
                UserId = userId
            });
        }

        var user = await _userService.GetUserByIdAsync(userId);
        return new JsonResult(new
        {
            UserId = userId,
            user.Username
        });
    }
}
