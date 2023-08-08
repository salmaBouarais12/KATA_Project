using KATA.Domain.Interfaces.Repositories;
using KATA.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace KATA.Dal.Repositories;

public class RoomRepository : IRoomRepository
{
    private readonly DbKataContext _dbKataContext;
    public RoomRepository(DbKataContext dbKataContext)
    {
        _dbKataContext = dbKataContext;
    }

    public async Task<IEnumerable<Room>> GetAllRoomsAsync()
    {
        return (await _dbKataContext.Rooms.ToListAsync()).Select(r => new Room
        {
            Id = r.Id,
            RoomName = r.RoomName
        });
    }

    public async Task<Room?> GetRoomByIdAsync(int id)
    {
        var rooms = await _dbKataContext.Rooms.SingleOrDefaultAsync(r => r.Id == id);
        if (rooms == null) return null;
        return new Room { Id = rooms.Id, RoomName = rooms.RoomName };
    }
    public async Task<Room> AddRoomsAsync(Room room)
    {
        var roomToAdd = new RoomEntity { RoomName = room.RoomName };
        _dbKataContext.Rooms.Add(roomToAdd);
        await _dbKataContext.SaveChangesAsync();
        return new Room { Id = roomToAdd.Id, RoomName = roomToAdd.RoomName };
    }
}
