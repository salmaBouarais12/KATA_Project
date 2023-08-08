using KATA.Domain.Models;

namespace KATA.Domain.Interfaces.Sevices;

public interface IRoomService
{
    Task<IEnumerable<Room>> GetAllRoomsAsync();
}
