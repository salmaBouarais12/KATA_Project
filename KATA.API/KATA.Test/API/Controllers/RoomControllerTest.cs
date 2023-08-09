using KATA.API.Controllers;
using KATA.API.DTO.Requests;
using KATA.API.DTO.Responses;
using KATA.Domain.Interfaces.Sevices;
using KATA.Domain.Models;
using KATA.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using NFluent;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
