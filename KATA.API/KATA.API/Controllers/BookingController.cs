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
            IEnumerable<Booking> bookings = await _bookingService.GetReservationsAsync();

            var bookingDetails = bookings.Select(b => new BookingResponse(b.Id, b.RoomId, b.PersonId, b.BookingDate, b.StartSlot, b.EndSlot));
            var bookingsResponse = new BookingsResponse(bookingDetails);
            return Ok(bookingsResponse);
        }

        // GET api/<BookingController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<BookingController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<BookingController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<BookingController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
