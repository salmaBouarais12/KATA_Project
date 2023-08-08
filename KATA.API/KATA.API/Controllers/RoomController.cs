using KATA.API.DTO.Requests;
using KATA.API.DTO.Responses;
using KATA.Domain.Interfaces.Sevices;
using KATA.Domain.Models;
using KATA.Domain.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KATA.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;

        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }
        // GET: api/<RoomController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var rooms = await _roomService.GetAllRoomsAsync();
            var roomDetails = rooms.Select(r => new RoomResponse(r.Id, r.RoomName));
            var RoomsResponse = new RoomsResponse(roomDetails);
            return Ok(RoomsResponse);
        }

        // GET api/<RoomController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var room = await _roomService.GetRoomByIdAsync(id);
            if (room == null) return NotFound();
            return Ok(new RoomResponse(room.Id, room.RoomName)); ;
        }

        // POST api/<RoomController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PostRoomRequest postRoomRequest)
        {
            if (postRoomRequest is null || !postRoomRequest.IsValid())
            {
                return BadRequest();
            }

            var room = new Room { RoomName = postRoomRequest.RoomName };
            var roomAdded = await _roomService.AddRoomsAsync(room);

            return Ok(new RoomResponse(roomAdded.Id, roomAdded.RoomName));
        }

        // PUT api/<RoomController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<RoomController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
