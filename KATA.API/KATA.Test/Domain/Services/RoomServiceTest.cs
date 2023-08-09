using KATA.Domain.Interfaces.Repositories;
using KATA.Domain.Models;
using KATA.Domain.Services;
using NFluent;
using NSubstitute;

namespace KATA.Test.Domain.Services;

public class RoomServiceTest
{
    [Fact]
    public async Task Should_Get_AllRooms()
    {
        //Arrange
        var listRooms = new List<Room>
    {
        new Room { Id = 13,RoomName = "White Room"},
        new Room {Id = 14 , RoomName = "Black Room" }
    };
        IRoomRepository roomRepository = Substitute.For<IRoomRepository>();
        roomRepository.GetAllRoomsAsync().Returns(listRooms);
        var roomService = new RoomService(roomRepository);

        //Act
        var allRooms = await (roomService.GetAllRoomsAsync());

        //Assert
        Check.That(allRooms).HasSize(2);
    }
}
