using KATA.Domain.Interfaces.Repositories;
using KATA.Domain.Interfaces.Sevices;
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

        [Fact]
        public async Task Should_NotBooking_Room_If_is_Not_Available()
        {
            var booking = new Booking { RoomId = 2, PersonId = 1, BookingDate = new DateTime(2023, 05, 10), StartSlot = 2, EndSlot = 3 };
            IBookingRepository bookingRepository = Substitute.For<IBookingRepository>();
            IPersonRepository personRepository = Substitute.For<IPersonRepository>();
            IRoomRepository roomRepository = Substitute.For<IRoomRepository>();
            var personService = new PersonService(personRepository);
            var roomService = new RoomService(roomRepository);
            _ = bookingRepository.DidNotReceive().AddReservationAsync(booking);
            _ = roomRepository.GetRoomByIdAsync(2);
            var bookingService = new BookingService(bookingRepository, personService, roomService);
            var newBooking = await bookingService.AddReservationAsync(booking);
            Check.That(newBooking).IsNotNull();
            Check.That(newBooking.ErrorMsg).HasSize(2);
            Assert.Null(newBooking.Booking);
        }

        [Fact]
        public async Task Should_Create_Reservation()
        {
            var booking = new Booking { RoomId = 2, PersonId = 1, BookingDate = new DateTime(2023, 05, 10), StartSlot = 2, EndSlot = 3 };
            IBookingRepository bookingRepository = Substitute.For<IBookingRepository>();
            IPersonRepository personRepository = Substitute.For<IPersonRepository>();
            IRoomRepository roomRepository = Substitute.For<IRoomRepository>();
            var personService = new PersonService(personRepository);
            var roomService = new RoomService(roomRepository);
            var rr = roomRepository.GetRoomByIdAsync(2);
            var pp = personRepository.GetPersonByIdAsync(1);
            var bookingForSubstitute = new Booking
            {
                Id = 1,
                PersonId = pp.Id,
                RoomId = rr.Id,
                StartSlot = booking.StartSlot,
                EndSlot = booking.EndSlot,
            };
            var bb = bookingRepository.AddReservationAsync(booking).Returns(bookingForSubstitute);
            var bookingService = new BookingService(bookingRepository, personService, roomService);
            var newBooking = await bookingService.AddReservationAsync(bookingForSubstitute);
            Check.That(newBooking).IsNotNull();
        }
    }
}
