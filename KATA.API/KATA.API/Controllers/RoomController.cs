using KATA.API.DTO.Requests;
using KATA.API.DTO.Responses;
using KATA.Domain.Interfaces.Sevices;
using KATA.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KATA.API.Controllers;

[Authorize]
[Route("api/rooms")]
[ApiController]
public class RoomController : ControllerBase
{
    private readonly IRoomService _roomService;
    private readonly ILogger<RoomController> _logger;

    public RoomController(IRoomService roomService, ILoggerFactory loggerFactory)
    {
        _roomService = roomService;
        _logger = loggerFactory.CreateLogger<RoomController>();
    }
    // GET: api/<RoomController>
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var rooms = await _roomService.GetAllRoomsAsync();
        var roomDetails = rooms.Select(r => new RoomResponse(r.Id, r.RoomName));
        var RoomsResponse = new RoomsResponse(roomDetails);
        _logger.LogInformation("Retrieved {count} rooms from the database.", rooms.Count());
        return Ok(RoomsResponse);
    }

    // GET api/<RoomController>/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        var room = await _roomService.GetRoomByIdAsync(id);

        if (room == null)
        {
            _logger.LogWarning("Room with ID {id} not found in the database.", id);
            return NotFound();
        }
        _logger.LogInformation("Retrieved room with ID {id} from the database.", id);
        return Ok(new RoomResponse(room.Id, room.RoomName)); ;
    }

    // POST api/<RoomController>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] PostRoomRequest postRoomRequest)
    {
        if (!ModelState.IsValid)
        {
            _logger.LogError("Failed to add a new room due to invalid model state.");
            return BadRequest();
        }

        var room = new Room { RoomName = postRoomRequest.RoomName };
        var roomAdded = await _roomService.AddRoomsAsync(room);
        _logger.LogInformation("Successfully added a new room with ID {id}.", roomAdded.Id);
        return Ok(roomAdded);
    }

    // PUT api/<RoomController>/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put([FromRoute] int id, [FromBody] PostRoomRequest postRoomRequest)
    {
        var room = new Room { RoomName = postRoomRequest.RoomName };
        if (!ModelState.IsValid)
        {
            _logger.LogError("Failed to update a room with ID {id} due to invalid model state.", id);
            return BadRequest();
        }
        var updateRoom = await _roomService.UpdateRoomsAsync(id, room);

        if (updateRoom == null)
        {
            _logger.LogWarning("Failed to update a room with ID {id} as the room was not found.", id);
            return NotFound();
        }
        else
        {
            _logger.LogInformation("Successfully updated the room with ID {id}.", id);
            return Ok(new RoomResponse(updateRoom.Id, updateRoom.RoomName));
        }
    }

    // DELETE api/<RoomController>/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var roomToBeDeleted = await _roomService.DeleteRoomsAsync(id);

        if (roomToBeDeleted == null)
        {
            _logger.LogWarning("Failed to delete room with ID {id} as the room was not found.", id);
            return NotFound();
        }

        _logger.LogInformation("Successfully deleted the room with ID {id}.", id);
        return Ok(new RoomResponse(roomToBeDeleted.Id, roomToBeDeleted.RoomName));
    }
}
