namespace MeetingRoomBookingSystem.Persistence.Models;

public class User : ModelBase
{
    public string Username { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;
}
