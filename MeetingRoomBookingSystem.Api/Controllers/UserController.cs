using MeetingRoomBookingSystem.Core.Extensions;
using MeetingRoomBookingSystem.Core.IServices;
using MeetingRoomBookingSystem.Core.Requests;
using MeetingRoomBookingSystem.Core.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MeetingRoomBookingSystem.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _service;
    private readonly ITokenService _tokenService;

    public UserController(IUserService service, ITokenService tokenService)
    {
        _service = service;
        _tokenService = tokenService;
    }

    [HttpGet("get-user-by-id")]
    public async Task<ActionResult<UserResponse>> GetUserByIdAsync(string userId)
    {
        var user = await _service.GetUserByIdAsync(userId);
        return Ok(user);
    }

    [HttpPost("add-user")]
    public async Task<ActionResult<UserResponse>> AddUserAsync([FromBody] AddUserRequest request)
    {
        var existingUser = await _service.GetUserByUsernameAsync(request.Username);
        if (existingUser != null) return Ok();

        var user = await _service.AddUserAsync(request.Username, request.Password);
        return Ok(user.ToResponse());
    }

    [HttpPost("delete-user")]
    [Authorize]
    public async Task<ActionResult<UserResponse>> DeleteUserAsync([FromBody] DeleteUserRequest request)
    {
        await _service.DeleteUserByIdAsync(request.UserId);
        return Ok();
    }

    [HttpPost("login")]
    public async Task<ActionResult<LoginResponse>> LoginAsync(LoginRequest request)
    {
        var userModel = await _service.LoginAsync(request.Username, request.Password);
        var (accessToken, refreshToken) = await GenerateTokens(userModel.Id.ToString());
        var loginResponse = new LoginResponse
        {
            Id = userModel.Id.ToString(),
            UserId = userModel.Id.ToString(),
            Username = userModel.Username,
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };
        return Ok(loginResponse);
    }

    private async Task<(string, string)> GenerateTokens(string userId)
    {
        var accessToken = await _tokenService.GenerateAccessTokenAsync(userId);
        var refreshToken = await _tokenService.GenerateRefreshTokenAsync(userId);
        return (accessToken, refreshToken);
    }
}
