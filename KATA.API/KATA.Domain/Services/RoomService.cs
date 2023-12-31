﻿using KATA.Domain.Interfaces.Repositories;
using KATA.Domain.Interfaces.Sevices;
using KATA.Domain.Models;

namespace KATA.Domain.Services;

public class RoomService : IRoomService
{
    private readonly IRoomRepository _roomRepository;
    public RoomService(IRoomRepository roomRepository)
    {
        _roomRepository = roomRepository;
    }

    public async Task<IEnumerable<Room>> GetAllRoomsAsync()
    {
        return await _roomRepository.GetAllRoomsAsync();
    }

    public async Task<Room?> GetRoomByIdAsync(int id)
    {
        return await _roomRepository.GetRoomByIdAsync(id);
    }

    public async Task<Room> AddRoomsAsync(Room room)
    {
        return await _roomRepository.AddRoomsAsync(room);
    }

    public async Task<Room?> UpdateRoomsAsync(int id, Room room)
    {
        return await _roomRepository.UpdateRoomsAsync(id,room);
    }

    public async Task<Room?> DeleteRoomsAsync(int id)
    {
        return await _roomRepository.DeleteRoomsAsync(id);
    }
}
