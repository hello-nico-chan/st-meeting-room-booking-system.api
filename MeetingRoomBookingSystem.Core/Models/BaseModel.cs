namespace MeetingRoomBookingSystem.Core.Models;

public class BaseModel
{
    public Guid Id { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}
