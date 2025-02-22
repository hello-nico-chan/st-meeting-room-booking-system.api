namespace MeetingRoomBookingSystem.Persistence.Models;

public class RefreshToken : ModelBase
{
    public Guid UserId { get; set; }

    public string Token { get; set; }

    public DateTime LastRequestTime { get; set; }
}
