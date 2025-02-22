using MeetingRoomBookingSystem.Core.Models;
using MeetingRoomBookingSystem.Core.Responses;

namespace MeetingRoomBookingSystem.Core.Extensions;

public static class UserModelExtension
{
    public static UserResponse ToResponse(this UserModel userModel)
    {
        return new UserResponse
        {
            Id = userModel.Id.ToString(),
            Username = userModel.Username
        };
    }
}
