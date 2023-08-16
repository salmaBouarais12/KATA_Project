using KATA.Domain.Interfaces.Repositories;
using KATA.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace KATA.Dal.Repositories;

public class BookingRepository : IBookingRepository
{
    private readonly DbKataContext _dbKataContext;

    public BookingRepository(DbKataContext dbKataContext)
    {
        _dbKataContext = dbKataContext;
    }

    public async Task<IEnumerable<Booking>> GetReservationsAsync()
    {
        return await _dbKataContext.Bookings.Select(b => new Booking
        {
            Id = b.Id,
            RoomId = b.RoomId,
            PersonId = b.PersonId,
            BookingDate = b.BookingDate,
            StartSlot = b.StartSlot,
            EndSlot = b.EndSlot
        }).ToListAsync(); ;
    }
    public async Task<Booking?> GetReservationByRoomIdAsync(int roomId)
    {
        var result = await _dbKataContext.Bookings.SingleOrDefaultAsync(b => b.RoomId == roomId);
        if (result is not null)
        {
            return new Booking
            {
                Id = result.Id,
                RoomId = result.RoomId,
                PersonId = result.PersonId,
                BookingDate = result.BookingDate,
                StartSlot = result.StartSlot,
                EndSlot = result.EndSlot
            };
        }
        return null;
    }
}
