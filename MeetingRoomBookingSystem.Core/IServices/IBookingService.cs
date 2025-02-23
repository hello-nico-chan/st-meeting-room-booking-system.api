using MeetingRoomBookingSystem.Core.Models;

namespace MeetingRoomBookingSystem.Core.IServices;

public interface IBookingService
{
    public Task<List<BookingModel>> GetBookingsAsync(string roomId);

    public Task CheckBooking(string roomId, DateTime startTime, DateTime endTime);

    public Task<BookingModel> BookingAsync(string roomId, string userId, string title, string participants, DateTime startTime, DateTime endTime);

    public Task DeleteBookingAsync(string bookingId);
}
