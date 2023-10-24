using KATA.API.DTO.Requests;
using KATA.API.DTO.Responses;
using KATA.Domain.Interfaces.Sevices;
using KATA.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KATA.API.Controllers;
[Authorize]
[Route("api/bookings")]
[ApiController]
public class BookingController : ControllerBase
{
    private readonly IBookingService _bookingService;
    private readonly ILogger<BookingController> _logger;

    public BookingController(IBookingService bookingService, ILoggerFactory loggerFactory)
    {
        _bookingService = bookingService;
        _logger = loggerFactory.CreateLogger<BookingController>();
    }

    // GET: api/<BookingController>
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var bookings = await _bookingService.GetReservationsAsync();

        var bookingDetails = bookings.Select(b => new BookingResponse(b.Id, b.RoomId, b.PersonId, b.BookingDate, b.StartSlot, b.EndSlot));
        var bookingsResponse = new BookingsResponse(bookingDetails);
        _logger.LogInformation("Retrieved {count} reservations from the database.", bookings.Count());
        return Ok(bookingsResponse);
    }

    [HttpGet("room/{roomId}")]
    public async Task<IActionResult> GetByRoomId([FromRoute] int roomId)
    {
        var booking = await _bookingService.GetReservationByRoomIdAsync(roomId);

        if (booking is not null)
        {
            _logger.LogInformation("Retrieved reservation with ID {id} for room with ID {roomId} from the database.", booking.Id, roomId);
            return Ok(new BookingResponse(booking.Id, booking.RoomId, booking.PersonId, booking.BookingDate, booking.StartSlot, booking.EndSlot));
        }

        _logger.LogWarning("Reservation for room with ID {roomId} not found in the database.", roomId);
        return NotFound();
    }

    // GET api/booking/id/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetBookingById([FromRoute] int id)
    {
        var booking = await _bookingService.GetReservationByIdAsync(id);
        if (booking == null) return NotFound();
        return Ok(new BookingResponse(id, booking.RoomId, booking.PersonId, booking.BookingDate, booking.StartSlot, booking.EndSlot));
    }

    // POST api/<BookingController>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] PostBookingRequest postBookingRequest)
    {
        if (!ModelState.IsValid)
        {
            _logger.LogError("Failed to add a new reservation due to invalid model state.");
            return BadRequest();
        }

        var reservation = new Booking();
        reservation.RoomId = postBookingRequest.RoomId;
        reservation.PersonId = postBookingRequest.PersonId;
        reservation.BookingDate = postBookingRequest.BookingDate;
        reservation.StartSlot = postBookingRequest.StartSlot;
        reservation.EndSlot = postBookingRequest.EndSlot;
        var addBooking = await _bookingService.AddReservationAsync(reservation);
        var reservationResponse = new ReservationResponse();
        //reservationResponse.Message = addBooking.ErrorMsg.ToList();
        if (addBooking.Booking != null)
        {
            reservationResponse.Reservation = new Booking
            {
                Id = addBooking.Booking.Id,
                RoomId = addBooking.Booking.RoomId,
                PersonId = addBooking.Booking.PersonId,
                BookingDate = addBooking.Booking.BookingDate,
                StartSlot = addBooking.Booking.StartSlot,
                EndSlot = addBooking.Booking.EndSlot
            };
        }

        reservationResponse.ListesCreneux = addBooking.ListOfReservation.Select(s => new SlotDTO { StartSlot = s.StartSlot, EndSlot = s.EndSlot });
        _logger.LogInformation("Successfully added a new reservation with ID {id}.", reservationResponse.Reservation?.Id);
        return Ok(addBooking);
    }

    // DELETE api/<BookingController>/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var bookingToDelete = await _bookingService.DeleteBookingsAsync(id);

        if (bookingToDelete == null)
        {
            _logger.LogWarning("Failed to delete booking with ID {id} as the booking was not found.", id);
            return NotFound("Booking not found.");
        }

        _logger.LogInformation("Successfully deleted the booking with ID {id}.", id);
        return Ok(new BookingResponse
            (
                bookingToDelete.Id,
                bookingToDelete.RoomId,
                bookingToDelete.PersonId,
                bookingToDelete.BookingDate,
                bookingToDelete.StartSlot,
                bookingToDelete.EndSlot
            ));
    }
}
