using KATA.Dal;
using KATA.Dal.Repositories;
using KATA.Domain.Models;
using Microsoft.EntityFrameworkCore;
using NFluent;

namespace KATA.Test.Dal.Repositories;

public class BookingRepositoryTest
{
    [Fact]
    public async Task Should_Get_Reservations()
    {
        var options = new DbContextOptionsBuilder<DbKataContext>()
            .UseInMemoryDatabase("when_requesting_bookings_on_repository")
            .Options;
          
        var fakeBookings = new[]
        {
            new BookingEntity { Id = 6, RoomId = 7, PersonId = 1 , BookingDate =new DateTime(2023, 05, 10),StartSlot =2,EndSlot=4 }
        };

        await using (var ctx = new DbKataContext(options))
        {
            ctx.Bookings.AddRange(fakeBookings);
            await ctx.SaveChangesAsync();
        }

        await using (var ctx = new DbKataContext(options))
        {
            var bookingRepository = new BookingRepository(ctx);
            var bookings = await bookingRepository.GetReservationsAsync();

            Check.That(bookings).HasSize(2);
        }
    }

    [Fact]
    public async Task Should_Get_ReservationByRoomId()
    {
        var options = new DbContextOptionsBuilder<DbKataContext>()
            .UseInMemoryDatabase("when_requesting_bookings_on_repository")
            .Options;

        var fakeBookings1 = new[]
        {
            new BookingEntity { Id = 2, RoomId =3, PersonId = 1 , BookingDate = new DateTime(2023, 05, 10),StartSlot =2, EndSlot=4 }
        };

        await using (var ctx = new DbKataContext(options))
        {
            ctx.Bookings.AddRange(fakeBookings1);
            await ctx.SaveChangesAsync();
        }

        await using (var ctx = new DbKataContext(options))
        {
            var bookingRepository = new BookingRepository(ctx);
            var booking = await bookingRepository.GetReservationByRoomIdAsync(3);

            Check.That(booking!.Id).Equals(2);
            Check.That(booking!.RoomId).Equals(3);
        }
    }
    [Fact]
    public async Task Should_Get_ReservationByRoomIdAndDate()
    {
        var options = new DbContextOptionsBuilder<DbKataContext>()
            .UseInMemoryDatabase("when_requesting_bookings_on_repository")
            .Options;

        var bookings = new[]
        {
            new BookingEntity { Id = 5, RoomId =4, PersonId = 1 , BookingDate = new DateTime(2023, 05, 10),StartSlot =2, EndSlot=4 }
        };

        var searchBooking = new SearchBooking { RoomId = 4, Date = new DateTime(2023, 05, 10) };
        await using (var ctx = new DbKataContext(options))
        {
            ctx.Bookings.AddRange(bookings);
            await ctx.SaveChangesAsync();
        }

        await using (var ctx = new DbKataContext(options))
        {
            var bookingRepository = new BookingRepository(ctx);
            var bookingByRoomandDate = await bookingRepository.GetReservationByRoomAndByDate(searchBooking);

            Check.That(bookings).HasSize(1);
        }
    }
}
