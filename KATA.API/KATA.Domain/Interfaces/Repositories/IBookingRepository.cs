using KATA.Domain.Models;

namespace KATA.Domain.Interfaces.Repositories;

public interface IBookingRepository
{
    Task<IEnumerable<Booking>> GetReservationsAsync();
}
