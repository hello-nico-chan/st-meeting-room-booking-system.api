using MeetingRoomBookingSystem.Core.Models;

namespace MeetingRoomBookingSystem.Core.IServices;

public interface IUserService
{
    public Task<UserModel> GetUserByIdAsync(string userId);

    public Task<UserModel> AddUserAsync(string username);

    public Task DeleteUserByIdAsync(string userId);

    public Task<UserModel> LoginAsync(string username, string code, string? registrationId = null);

    public Task CheckUserExistAsync(string accountType, string accountId);
}
