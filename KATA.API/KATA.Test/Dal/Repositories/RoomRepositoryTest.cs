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
}