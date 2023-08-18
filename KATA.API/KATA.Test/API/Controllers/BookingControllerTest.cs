using KATA.API.Controllers;
using KATA.API.DTO.Requests;
using KATA.Domain.Interfaces.Sevices;
using KATA.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using NFluent;
using NSubstitute;

namespace KATA.Test.API.Controllers;

public class BookingControllerTest
{
    private readonly IBookingService bookingService = Substitute.For<IBookingService>();
    [Fact]
    public async Task Should_Get_AllReservation()
    {
        var listBookings = new List<Booking>
        {
            new Booking { RoomId = 2 , PersonId =1, BookingDate = new DateTime(2023,05,10),StartSlot = 2 ,EndSlot = 3},
            new Booking { RoomId = 5 , PersonId =3, BookingDate = new DateTime(2023,04,12),StartSlot = 1 ,EndSlot = 4},
             new Booking { RoomId = 7 , PersonId =3, BookingDate = new DateTime(2022,04,15),StartSlot = 3 ,EndSlot = 6}
        };
        bookingService.GetReservationsAsync().Returns(listBookings);

        var bookingController = new BookingController(bookingService);
        var result = await bookingController.Get();
        Check.That(result).IsNotNull();
        Check.That(result).IsInstanceOf<OkObjectResult>();
    }

    [Fact]
    public async Task Should_Not_Create_Booking_And_Return_400_When_Bad_Request()
    {
        var bookingRequest = new PostBookingRequest();
        var bookingController = new BookingController(bookingService);
        bookingController.ModelState.AddModelError("", "");
        var result = await bookingController.Post(bookingRequest);

        Check.That(result).IsNotNull();
        Check.That(result).IsInstanceOf<BadRequestResult>();
    }

    [Fact]
    public async Task Should_Not_Delete_Booking_And_Return_404_When_Booking_Doesnt_Exist()
    {
        var bookingController = new BookingController(bookingService);
        var result = await bookingController.Delete(15);
        Check.That(result).IsInstanceOf<NotFoundResult>();
    }

    [Fact]
    public async Task Should_Delete_Booking()
    {
        var booking = new Booking { RoomId = 2, PersonId = 1, BookingDate = new DateTime(2023, 05, 10), StartSlot = 2, EndSlot = 3 };

        var bookingController = new BookingController(bookingService);
        var bookingToDeletd = await bookingController.Delete(booking.Id);

        Check.That(bookingToDeletd).IsNotNull();
        Check.That(bookingToDeletd).IsInstanceOf<NotFoundResult>();
    }
}
