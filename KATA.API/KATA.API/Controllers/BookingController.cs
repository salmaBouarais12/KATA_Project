using KATA.API.DTO.Requests;
using KATA.API.DTO.Responses;
using KATA.Domain.Interfaces.Sevices;
using KATA.Domain.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KATA.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        // GET: api/<BookingController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var bookings = await _bookingService.GetReservationsAsync();

            var bookingDetails = bookings.Select(b => new BookingResponse(b.Id, b.RoomId, b.PersonId, b.BookingDate, b.StartSlot, b.EndSlot));
            var bookingsResponse = new BookingsResponse(bookingDetails);
            return Ok(bookingsResponse);
        }

        // GET api/<BookingController>/5
        [HttpGet("{roomId}")]
        public async Task<IActionResult> Get([FromRoute] int roomId)
        {
            var booking = await _bookingService.GetReservationByRoomIdAsync(roomId);
            if (booking is not null)
            {
                return Ok(new BookingResponse(booking.Id, booking.RoomId, booking.PersonId, booking.BookingDate, booking.StartSlot, booking.EndSlot));
            }
            return NotFound();
        }

        // POST api/<BookingController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PostBookingRequest postBookingRequest)
        {
            if (!ModelState.IsValid)
            {
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
            reservationResponse.Message = addBooking.ErrorMsg.ToList();
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
            return Ok(addBooking);
        }

        // DELETE api/<BookingController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var bookingToDelete = await _bookingService.DeleteBookingsAsync(id);
            if (bookingToDelete == null)
                return NotFound();
            return Ok(new BookingResponse
                (bookingToDelete.Id,
                bookingToDelete.RoomId,
                bookingToDelete.PersonId,
                bookingToDelete.BookingDate,
                bookingToDelete.StartSlot,
                bookingToDelete.EndSlot));
        }
    }
}
