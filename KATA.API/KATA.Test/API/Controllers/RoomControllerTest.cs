using KATA.API.Controllers;
using KATA.API.DTO.Requests;
using KATA.API.DTO.Responses;
using KATA.Domain.Interfaces.Sevices;
using KATA.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using NFluent;
using NSubstitute;

namespace KATA.Test.API.Controllers
{
    public class RoomControllerTest
    {
        private readonly IRoomService roomService = Substitute.For<IRoomService>();
        [Fact]
        public async Task Should_Get_RoomById()
        {
            //Arrange
            var room = new Room { Id = 20, RoomName = "Black Room" };
            roomService.GetRoomByIdAsync(Arg.Is<int>(x => x == room.Id)).Returns(room);

            var roomController = new RoomController(roomService);
            var roomById = await roomController.Get(room.Id);
            Check.That(roomById).IsNotNull();
            Check.That(roomById).IsInstanceOf<OkObjectResult>();

            Check.That(((OkObjectResult)roomById).Value).IsNotNull();
            Check.That(((OkObjectResult)roomById).Value).IsInstanceOf<RoomResponse>();
            Check.That(((RoomResponse)((OkObjectResult)roomById).Value!).Id).IsEqualTo(20);
        }

        [Fact]
        public async Task Should_Create_Room()
        {
            var room = new PostRoomRequest { RoomName = "White Room" };

            var roomController = new RoomController(roomService);
            var result = await roomController.Post(room);

            Check.That(result).IsNotNull();
            Check.That(((ObjectResult)result).StatusCode).IsEqualTo(200);
        }

        [Fact]
        public async Task Should_Not_Create_Room_And_Return_400_When_Bad_Request()
        {
            var roomRequest = new PostRoomRequest();
            var roomController = new RoomController(roomService);
            roomController.ModelState.AddModelError("", "");
            var result = await roomController.Post(roomRequest);

            Check.That(result).IsNotNull();
            Check.That(result).IsInstanceOf<BadRequestResult>();
        }

        [Fact]
        public async Task Should_Not_Update_Room_And_Return_404_When_Room_Doesnt_Exist()
        {
            var room = new PostRoomRequest { RoomName = "Black Room" };

            var roomController = new RoomController(roomService);
            var result = await roomController.Put(35, room);

            Check.That(result).IsNotNull();
            Check.That(result).IsInstanceOf<NotFoundResult>();
        }

        [Fact]
        public async Task Should_Not_Delete_Room_And_Return_404_When_Room_Doesnt_Exist()
        {
            var roomontroller = new RoomController(roomService);
            var result = await roomontroller.Delete(15);
            Check.That(result).IsInstanceOf<NotFoundResult>();
        }

        [Fact]
        public async Task Should_Delete_Room()
        {
            var room = new Room { Id = 33 , RoomName = "Black Room" };
            roomService.GetRoomByIdAsync(Arg.Is<int>(x => x == room.Id)).Returns(room);

            var roomController = new RoomController(roomService);
            var roomToDeletd = await roomController.Delete(room.Id);

            Check.That(roomToDeletd).IsNotNull();
            Check.That(roomToDeletd).IsInstanceOf<NotFoundResult>();
        }

    }
}
