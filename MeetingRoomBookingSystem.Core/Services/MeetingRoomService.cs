using MeetingRoomBookingSystem.Core.IRepositories;
using MeetingRoomBookingSystem.Core.IServices;
using MeetingRoomBookingSystem.Core.Models;
using MeetingRoomBookingSystem.Persistence.Models;

namespace MeetingRoomBookingSystem.Core.Services;

public class MeetingRoomService : IMeetingRoomService
{
    private readonly IGeneralRepository<MeetingRoom> _repository;

    public MeetingRoomService(IGeneralRepository<MeetingRoom> repository)
    {
        _repository = repository;
    }

    public async Task<MeetingRoomModel> AddMeetingRoomAsync(string name, string location, int capacity, string type, string remark)
    {
        var meetingRoomModel = new MeetingRoomModel
        {
            Name = name,
            Location = location,
            Capacity = capacity,
            Type = type,
            Remark = remark
        };

        var meetingRoomEntity = new MeetingRoom()
        {
            Name = name,
            Location = location,
            Capacity = capacity,
            Type = type,
            Remark = remark
        };

        await _repository.InsertAsync(meetingRoomEntity);
        await _repository.SaveAsync();

        meetingRoomModel.Id = meetingRoomEntity.Id;

        return meetingRoomModel;
    }

    public async Task<MeetingRoomModel> EditMeetingRoomAsync(string id, string name, string location, int capacity, string type, string remark)
    {
        var meetingRooms = await _repository.GetAsync(x => x.Id == Guid.Parse(id));
        var meetingRoom = meetingRooms.FirstOrDefault()!;

        meetingRoom.Name = name;
        meetingRoom.Location = location;
        meetingRoom.Capacity = capacity;
        meetingRoom.Type = type;
        meetingRoom.Remark = remark;

        _repository.Update(meetingRoom);
        await _repository.SaveAsync();

        return new MeetingRoomModel()
        {
            Id = meetingRoom.Id,
            Name = meetingRoom.Name,
            Location = meetingRoom.Location,
            Capacity = meetingRoom.Capacity,
            Type = meetingRoom.Type,
            Remark = meetingRoom.Remark,
            CreatedAt = meetingRoom.CreatedAt,
            UpdatedAt = meetingRoom.UpdatedAt
        };
    }

    public async Task DeleteMeetingRoomByIdAsync(string id)
    {
        var meetingRooms = await _repository.GetAsync(x => x.Id == Guid.Parse(id));
        var meetingRoom = meetingRooms.FirstOrDefault() ?? throw new Exception();
        _repository.Delete(meetingRoom);
        await _repository.SaveAsync();
    }

    public async Task<List<MeetingRoomModel>> GetAllMeetingRoomsAsync()
    {
        var meetingRooms = await _repository.GetAsync(x => true);
        return meetingRooms.Select(meetingRoom => new MeetingRoomModel()
        {
            Id = meetingRoom.Id,
            Name = meetingRoom.Name,
            Location = meetingRoom.Location,
            Capacity = meetingRoom.Capacity,
            Type = meetingRoom.Type,
            Remark = meetingRoom.Remark,
            CreatedAt = meetingRoom.CreatedAt,
            UpdatedAt = meetingRoom.UpdatedAt
        }).ToList();
    }

    public async Task<MeetingRoomModel?> GetMeetingRoomByIdAsync(string id)
    {
        var meetingRooms = await _repository.GetAsync(x => x.Id == Guid.Parse(id));
        var meetingRoom = meetingRooms.FirstOrDefault();

        return meetingRoom == null ? null : new MeetingRoomModel
        {
            Id = meetingRoom.Id,
            Name = meetingRoom.Name,
            Location = meetingRoom.Location,
            Capacity = meetingRoom.Capacity,
            Type = meetingRoom.Type,
            Remark = meetingRoom.Remark,
            CreatedAt = meetingRoom.CreatedAt,
            UpdatedAt = meetingRoom.UpdatedAt
        };
    }
}
