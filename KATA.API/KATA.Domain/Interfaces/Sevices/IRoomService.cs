using KATA.Domain.Models;

namespace KATA.Domain.Interfaces.Sevices;

public interface IRoomService
{
    Task<IEnumerable<Room>> GetAllRoomsAsync();
    Task<Room?> GetRoomByIdAsync(int id);
    Task<Room> AddRoomsAsync(Room room);
    Task<Room> UpdateRoomsAsync(int id, Room room);
}
