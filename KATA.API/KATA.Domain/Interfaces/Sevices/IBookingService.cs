using KATA.Domain.Models;

namespace KATA.Domain.Interfaces.Sevices;

public interface IBookingService
{
    Task<IEnumerable<Booking>> GetReservationsAsync();
}
