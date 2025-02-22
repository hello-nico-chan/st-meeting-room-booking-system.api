namespace MeetingRoomBookingSystem.Core.Responses;

public class LoginResponse : BaseResponse
{
    public string UserId { get; set; }

    public string Username { get; set; }

    public bool IsAdmin { get; set; }

    public string AccessToken { get; set; }

    public string RefreshToken { get; set; }
}
