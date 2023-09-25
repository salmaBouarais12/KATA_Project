using KATA.Domain.Models;

namespace KATA.Domain.Interfaces.Sevices;

public interface IBookingService
{
    Task<IEnumerable<Booking>> GetReservationsAsync();
    Task<Booking?> GetReservationByRoomIdAsync(int roomId);
    Task<Booking?> GetReservationByIdAsync(int id);
    Task<CreationBookingResult> AddReservationAsync(Booking reservation);
    Task<IEnumerable<Booking?>> GetReservationByRommAndByDate(SearchBooking searchBooking);
    Task<Booking?> DeleteBookingsAsync(int id);
}
