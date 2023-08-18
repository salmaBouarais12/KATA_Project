using KATA.Domain.Interfaces.Repositories;
using KATA.Domain.Models;
using KATA.Domain.Services;
using NFluent;
using NSubstitute;

namespace KATA.Test.Domain.Services
{
    public class BookingServiceTest
    {
        [Fact]
        public async Task Should_Get_AllBookings()
        {
            //Arrange
            var listBookings = new List<Booking>
        {
            new Booking { RoomId = 2 , PersonId =1, BookingDate = new DateTime(2023,05,10),StartSlot = 2 ,EndSlot = 3},
            new Booking { RoomId = 5 , PersonId =3, BookingDate = new DateTime(2023,04,12),StartSlot = 1 ,EndSlot = 4},
             new Booking { RoomId = 7 , PersonId =3, BookingDate = new DateTime(2022,04,15),StartSlot = 3 ,EndSlot = 6}
        };
            IBookingRepository bookingRepository = Substitute.For<IBookingRepository>();
            bookingRepository.GetReservationsAsync().Returns(listBookings);
            var bookingService = new BookingService(bookingRepository, null, null);

            //Act
            var bookings = await (bookingService.GetReservationsAsync());

            //Assert
            Check.That(bookings).HasSize(3);
        }
    }
}
