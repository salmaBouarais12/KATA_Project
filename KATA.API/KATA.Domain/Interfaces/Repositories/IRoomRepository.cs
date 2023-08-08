using KATA.Domain.Models;

namespace KATA.Domain.Interfaces.Repositories;

public interface IRoomRepository
{
    Task<IEnumerable<Room>> GetAllRoomsAsync();
    Task<Room?> GetRoomByIdAsync(int id);
}
