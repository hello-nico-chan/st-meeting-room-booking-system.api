namespace MeetingRoomBookingSystem.Core.Requests;

public class AddMeetingRoomRequest
{
    public string Name { get; set; } = string.Empty;

    public string Location { get; set; } = string.Empty;
}
