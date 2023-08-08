﻿using KATA.Domain.Interfaces.Repositories;
using KATA.Domain.Models;
using Microsoft.EntityFrameworkCore;

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
}