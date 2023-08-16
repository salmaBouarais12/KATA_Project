using KATA.Domain.Models;

namespace KATA.Domain.Interfaces.Repositories;

public interface IBookingRepository
{
    Task<IEnumerable<Booking>> GetReservationsAsync();
    Task<Booking?> GetReservationByRoomIdAsync(int roomId);
    Task<Booking> AddReservationAsync(Booking reservation);
    Task<IEnumerable<Booking>> GetReservationByRommAndByDate(SearchBooking searchBooking);

}
