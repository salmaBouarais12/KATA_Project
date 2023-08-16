using KATA.Domain.Interfaces.Repositories;
using KATA.Domain.Interfaces.Sevices;
using KATA.Domain.Models;

namespace KATA.Domain.Services;

public class BookingService : IBookingService
{
    private readonly IBookingRepository _bookingRepository;
    public BookingService(IBookingRepository bookingRepository)
    {
        _bookingRepository = bookingRepository;
    }

    public async Task<IEnumerable<Booking>> GetReservationsAsync()
    {
        return await _bookingRepository.GetReservationsAsync();
    }

    public async Task<Booking?> GetReservationByRoomIdAsync(int roomId)
    {
        return await _bookingRepository.GetReservationByRoomIdAsync(roomId);
    }
}
