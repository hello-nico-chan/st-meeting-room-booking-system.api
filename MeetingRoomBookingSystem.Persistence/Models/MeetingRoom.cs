namespace MeetingRoomBookingSystem.Persistence.Models;

public class MeetingRoom : ModelBase
{
    public string Name { get; set; } = string.Empty;

    public string Location { get; set; } = string.Empty;
}
