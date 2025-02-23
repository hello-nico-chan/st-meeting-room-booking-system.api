namespace MeetingRoomBookingSystem.Core.Requests;

public class AddUserRequest
{
    public string Username { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public bool IsAdmin { get; set; }
}
