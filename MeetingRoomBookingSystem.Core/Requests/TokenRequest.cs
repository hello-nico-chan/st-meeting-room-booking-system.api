namespace MeetingRoomBookingSystem.Core.Requests;

public class TokenRequest
{
    public string UserId { get; set; }

    public string? RefreshToken { get; set; }
}
