using KATA.Domain.Models;

namespace KATA.Domain.Interfaces.Repositories;

public interface IRoomRepository
{
    Task<IEnumerable<Room>> GetAllRoomsAsync();
    Task<Room?> GetRoomByIdAsync(int id);
    Task<Room> AddRoomsAsync(Room room);
    Task<Room> UpdateRoomsAsync(int id, Room room);
    Task<Room> DeleteRoomsAsync(int id);
}
