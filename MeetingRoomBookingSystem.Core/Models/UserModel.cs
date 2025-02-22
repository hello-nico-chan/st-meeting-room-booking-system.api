
namespace MeetingRoomBookingSystem.Core.Models;

public class UserModel : BaseModel
{
    public string Username { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;
}
