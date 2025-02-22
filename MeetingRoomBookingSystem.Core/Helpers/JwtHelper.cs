using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace MeetingRoomBookingSystem.Core.Helpers;

public class JwtHelper
{
    public static string? GetUserIdFromToken(HttpContext httpContext)
    {
        var userId = string.Empty;
        var authHeader = httpContext.Request.Headers["Authorization"].ToString();

        if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer ")) return userId;
        var token = authHeader["Bearer ".Length..].Trim();

        var jwtSecurityToken = new JwtSecurityToken(token);
        userId = jwtSecurityToken.Payload[ClaimTypes.NameIdentifier].ToString();

        return userId;
    }
}
