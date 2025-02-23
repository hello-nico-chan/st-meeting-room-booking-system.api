namespace MeetingRoomBookingSystem.Core.Responses;

public class UserResponse : BaseResponse
{
    public string Username { get; set; } = string.Empty;

    public bool IsAdmin { get; set; }
}
