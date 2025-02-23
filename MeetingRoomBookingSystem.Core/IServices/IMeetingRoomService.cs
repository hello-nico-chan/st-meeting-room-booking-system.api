using MeetingRoomBookingSystem.Core.Models;

namespace MeetingRoomBookingSystem.Core.IServices;

public interface IMeetingRoomService
{
    public Task<MeetingRoomModel> AddMeetingRoomAsync(string name, string location, int capacity, string type, string remark);
    
    public Task<MeetingRoomModel> EditMeetingRoomAsync(string id, string name, string location, int capacity, string type, string remark);

    public Task<List<MeetingRoomModel>> GetAllMeetingRoomsAsync();

    public Task<MeetingRoomModel?> GetMeetingRoomByIdAsync(string id);

    public Task DeleteMeetingRoomByIdAsync(string id);
}
