using MeetingRoomBookingSystem.Core.IRepositories;
using MeetingRoomBookingSystem.Core.IServices;
using MeetingRoomBookingSystem.Core.Models;
using MeetingRoomBookingSystem.Persistence.Models;

namespace MeetingRoomBookingSystem.Core.Services;

public class UserService : IUserService
{
    private readonly IGeneralRepository<User> _repository;

    public UserService(IGeneralRepository<User> repository)
    {
        _repository = repository;
    }

    public async Task<UserModel?> GetUserByIdAsync(string userId)
    {
        var users = await _repository.GetAsync(x => x.Id == Guid.Parse(userId));
        var user = users.FirstOrDefault();

        return user == null ? null : new UserModel
        {
            Id = user.Id,
            CreatedAt = user.CreatedAt,
            UpdatedAt = user.UpdatedAt,
            Username = user.Username,
        };
    }

    public async Task<UserModel?> GetUserByUsernameAsync(string username)
    {
        var users = await _repository.GetAsync(x => x.Username == username);
        var user = users.FirstOrDefault();

        return user == null ? null : new UserModel
        {
            Id = user.Id,
            CreatedAt = user.CreatedAt,
            UpdatedAt = user.UpdatedAt,
            Username = user.Username,
        };
    }

    public async Task<UserModel> AddUserAsync(string username, string password)
    {
        var userModel = new UserModel
        {
            Username = username,
            Password = password
        };

        var userEntity = new User()
        {
            Username = username,
            Password = password
        };

        await _repository.InsertAsync(userEntity);
        await _repository.SaveAsync();

        userModel.Id = userEntity.Id;

        return userModel;
    }

    public async Task DeleteUserByIdAsync(string userId)
    {
        var users = await _repository.GetAsync(x => x.Id == Guid.Parse(userId));
        var user = users.FirstOrDefault() ?? throw new Exception();
        _repository.Delete(user);
        await _repository.SaveAsync();
    }

    public async Task<UserModel> LoginAsync(string accountId, string password)
    {
        var users = await _repository.GetAsync(user => user.Id == Guid.Parse(accountId) && user.Password == password);
        var user = users.FirstOrDefault() ?? throw new Exception();

        user.UpdatedAt = DateTime.UtcNow;
        _repository.Update(user);

        await _repository.SaveAsync();

        return new UserModel
        {
            Id = user.Id,
            CreatedAt = user.CreatedAt,
            UpdatedAt = user.UpdatedAt,
            Username = user.Username,
        };
    }

    public async Task CheckUserExistAsync(string accountType, string accountId)
    {
        var users = await _repository.GetAsync(user => user.Id == Guid.Parse(accountId));
        if (users.Any())
        {
            throw new Exception();
        }
    }
}
