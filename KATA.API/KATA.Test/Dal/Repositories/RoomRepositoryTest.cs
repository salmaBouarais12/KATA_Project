using KATA.Dal;
using KATA.Dal.Repositories;
using Microsoft.EntityFrameworkCore;
using NFluent;
namespace KATA.Test.Dal.Repositories;

public class RoomRepositoryTest
{
    [Fact]
    public async Task Should_Get_Rooms()
    {
        var options = new DbContextOptionsBuilder<DbKataContext>()
            .UseInMemoryDatabase("when_requesting_rooms_on_repository")
            .Options;

        var fakeRooms = new[]
        {
            new RoomEntity { Id = 2, RoomName = "White Room" }
        };

        await using (var ctx = new DbKataContext(options))
        {
            ctx.Rooms.AddRange(fakeRooms);
            await ctx.SaveChangesAsync();
        }

        await using (var ctx = new DbKataContext(options))
        {
            var roomRepository = new RoomRepository(ctx);
            var rooms = await roomRepository.GetAllRoomsAsync();

            Check.That(rooms).HasSize(1);
        }
    }

    [Fact]
    public async Task Should_Get_RoomById()
    {
        var options = new DbContextOptionsBuilder<DbKataContext>()
            .UseInMemoryDatabase("when_requesting_roomById_on_repository")
            .Options;

        var rooms = new[]
        {
            new RoomEntity { Id = 3, RoomName = "Black Room" }
        };

        await using (var ctx = new DbKataContext(options))
        {
            ctx.Rooms.AddRange(rooms);
            await ctx.SaveChangesAsync();
        }

        await using (var ctx = new DbKataContext(options))
        {
            var roomRepository = new RoomRepository(ctx);
            var room = await roomRepository.GetRoomByIdAsync(3);

            Check.That(room!.Id).Equals(3);
            Check.That(room!.RoomName).Equals("Black Room");
        }
    }
}