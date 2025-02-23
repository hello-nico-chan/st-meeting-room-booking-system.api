using MeetingRoomBookingSystem.Core.IRepositories;
using MeetingRoomBookingSystem.Core.IServices;
using MeetingRoomBookingSystem.Core.Models;
using MeetingRoomBookingSystem.Persistence.Models;

namespace MeetingRoomBookingSystem.Core.Services;

public class BookingService : IBookingService
{
    private readonly IGeneralRepository<Booking> _repository;

    public BookingService(IGeneralRepository<Booking> repository)
    {
        _repository = repository;
    }

    public async Task<BookingModel> BookingAsync(string roomId, string userId, string title, string participants, DateTime startTime, DateTime endTime)
    {
        var bookingModel = new BookingModel()
        {
            MeetingRoomId = roomId,
            UserId = userId,
            Title = title,
            Participants = participants,
            StartTime = startTime,
            EndTime = endTime
        };

        var bookingEntity = new Booking()
        {
            MeetingRoomId = roomId,
            UserId = userId,
            Title = title,
            Participants = participants,
            StartTime = startTime,
            EndTime = endTime
        };

        await _repository.InsertAsync(bookingEntity);
        await _repository.SaveAsync();

        bookingModel.Id = bookingEntity.Id;

        return bookingModel;
    }

    public async Task DeleteBookingAsync(string bookingId)
    {
        var bookings = await _repository.GetAsync(x => x.Id == Guid.Parse(bookingId));
        var booking = bookings.FirstOrDefault() ?? throw new Exception();
        _repository.Delete(booking);
        await _repository.SaveAsync();
    }

    public async Task<List<BookingModel>> GetBookingsAsync(string roomId)
    {
        var bookings = await _repository.GetAsync(x => x.MeetingRoomId == roomId);
        return bookings.Select(booking => new BookingModel()
        {
            Id = booking.Id,
            UserId = booking.UserId,
            CreatedAt = booking.CreatedAt,
            UpdatedAt = booking.UpdatedAt,
            MeetingRoomId = booking.MeetingRoomId,
            Title = booking.Title,
            Participants = booking.Participants,
            StartTime = booking.StartTime,
            EndTime = booking.EndTime
        }).ToList();
    }

    public async Task CheckBooking(string roomId, DateTime startTime, DateTime endTime)
    {
        var existingBooking = await _repository.GetAsync(x => x.MeetingRoomId == roomId && x.StartTime < endTime && x.EndTime > startTime);
        if (existingBooking.Any())
        {
            throw new Exception("409");
        }
    }
}
