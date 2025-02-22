namespace MeetingRoomBookingSystem.Core.Requests;

public class TokenRequest
{
    public string UserId { get; set; } = string.Empty;

    public string? RefreshToken { get; set; }
}
