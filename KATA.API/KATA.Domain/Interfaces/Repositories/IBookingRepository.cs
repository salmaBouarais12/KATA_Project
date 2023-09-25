using KATA.Domain.Models;

namespace KATA.Domain.Interfaces.Repositories;

public interface IBookingRepository
{
    Task<IEnumerable<Booking>> GetReservationsAsync();
    Task<Booking?> GetReservationByRoomIdAsync(int roomId);
    Task<Booking> AddReservationAsync(Booking reservation);
    Task<Booking?> GetReservationByIdAsync(int id);
    Task<IEnumerable<Booking>> GetReservationByRoomAndByDate(SearchBooking searchBooking);
    Task<Booking?> DeleteBookingsAsync(int id);

}
