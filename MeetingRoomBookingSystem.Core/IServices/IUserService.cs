using MeetingRoomBookingSystem.Core.Models;

namespace MeetingRoomBookingSystem.Core.IServices;

public interface IUserService
{
    public Task<UserModel?> GetUserByIdAsync(string userId);
    
    public Task<UserModel?> GetUserByUsernameAsync(string username);

    public Task<UserModel> AddUserAsync(string username, string password);

    public Task DeleteUserByIdAsync(string userId);

    public Task<UserModel> LoginAsync(string username, string password);

    public Task CheckUserExistAsync(string accountType, string accountId);
}
