namespace MeetingRoomBookingSystem.Core.IServices;

public interface ITokenService
{
    public Task<string> GenerateAccessTokenAsync(string userId);

    public Task<string> GenerateRefreshTokenAsync(string userId);

    public Task<string> GetRefreshTokenAsync(string userId);

    public Task<string> UpdateRefreshTokenAsync(string userId);

    public Task UpdateLastRequestTimeAsync(string userId);

    public Task CheckRefreshTokenExpired(string userId);
}
