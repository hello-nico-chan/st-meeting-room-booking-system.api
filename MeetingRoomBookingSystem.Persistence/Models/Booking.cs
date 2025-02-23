namespace MeetingRoomBookingSystem.Persistence.Models;

public class Booking : ModelBase
{
    public string UserId { get; set; } = string.Empty;

    public string MeetingRoomId { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;

    public string Participants { get; set; } = string.Empty;

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }
}
