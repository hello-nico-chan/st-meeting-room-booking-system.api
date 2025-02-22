namespace MeetingRoomBookingSystem.Core.Responses;

public class LoginResponse : BaseResponse
{
    public string UserId { get; set; } = string.Empty;

    public string Username { get; set; } = string.Empty;

    public bool IsAdmin { get; set; }

    public string AccessToken { get; set; } = string.Empty;

    public string RefreshToken { get; set; } = string.Empty;
}
