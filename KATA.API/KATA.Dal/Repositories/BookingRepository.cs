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

    public async Task<Booking> AddReservationAsync(Booking reservation)
    {
        var booking = new BookingEntity
        {
            Id = reservation.Id,
            RoomId = reservation.RoomId,
            PersonId = reservation.PersonId,
            BookingDate = reservation.BookingDate,
            StartSlot = reservation.StartSlot,
            EndSlot = reservation.EndSlot
        };

        _dbKataContext.Bookings.Add(booking);
        await _dbKataContext.SaveChangesAsync();

        return new Booking
        {
            Id = booking.Id,
            RoomId = booking.RoomId,
            PersonId = booking.PersonId,
            BookingDate = booking.BookingDate,
            StartSlot = booking.StartSlot,
            EndSlot = booking.EndSlot
        };
    }

    public async Task<IEnumerable<Booking>> GetReservationByRommAndByDate(SearchBooking searchBooking)
    {
        return await _dbKataContext.Bookings.
            Where(b => b.RoomId == searchBooking.RoomId && b.BookingDate.Date == searchBooking.Date.Date).
            Select(b => new Booking
            {
                Id = b.Id,
                RoomId = b.RoomId,
                PersonId = b.PersonId,
                BookingDate = b.BookingDate,
                StartSlot = b.StartSlot,
                EndSlot = b.EndSlot

            }).OrderBy(b => b.StartSlot).ToListAsync();
    }
}
