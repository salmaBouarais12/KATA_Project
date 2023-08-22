using KATA.Dal;
using KATA.Dal.Repositories;
using Microsoft.EntityFrameworkCore;
using NFluent;

namespace KATA.Test.Dal.Repositories
{
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
                new BookingEntity { Id = 1, RoomId =2, PersonId = 1 , BookingDate =new DateTime(),StartSlot =2,EndSlot=4 }
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

                Check.That(bookings).HasSize(1);
            }
        }
    }
}
