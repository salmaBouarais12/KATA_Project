using Azure;
using KATA.API.Controllers;
using KATA.API.DTO.Requests;
using KATA.API.DTO.Responses;
using KATA.Domain.Interfaces.Repositories;
using KATA.Domain.Interfaces.Sevices;
using KATA.Domain.Models;
using KATA.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using NFluent;
using NSubstitute;

namespace KATA.Test.API.Controllers;

public class PersonControllerTest
{
    private readonly IPersonService personService = Substitute.For<IPersonService>();
    [Fact]
    public async Task Should_Get_PersonById()
    {
        //Arrange
        var Person = new Person { Id = 8, FirstName = "Salma", LastName = "BOUARAIS" };
        personService.GetPersonByIdAsync(Arg.Is<int>(x => x == Person.Id)).Returns(Person);
        //personService.GetPersonByIdAsync(Arg.Any<int>()).Returns(Person);

        var personController = new PersonController(personService);
        var personById = await personController.Get(Person.Id);
        Check.That(personById).IsNotNull();
        Check.That(personById).IsInstanceOf<OkObjectResult>();

        Check.That(((OkObjectResult)personById).Value).IsNotNull();
        Check.That(((OkObjectResult)personById).Value).IsInstanceOf<PersonResponse>();
        Check.That(((PersonResponse)((OkObjectResult)personById).Value!).Id).IsEqualTo(8);
    }
    [Fact]
    public async Task Should_Get_Error404_WhenPersonDoesNotExist()
    {
        var personController = new PersonController(personService);
        var result = await personController.Get(default);

        Check.That(result).IsNotNull();
        Check.That(result).IsInstanceOf<NotFoundResult>();
    }

    [Fact]
    public async Task Should_Create_Person()
    {
        var person = new PostPersonRequest { FirstName = "Salma", LastName = "BOUARAIS" };

        var personController = new PersonController(personService);
        var result = await personController.Post(person);

        Check.That(result).IsNotNull();
        Check.That(((ObjectResult)result).StatusCode).IsEqualTo(200);
    }

    [Fact]
    public async Task Should_Not_Create_Person_And_Return_400_When_Bad_Request()
    {
        var personRequest = new PostPersonRequest();
        var personController = new PersonController(personService);
        var result = await personController.Post(personRequest);

        Check.That(result).IsNotNull();
        Check.That(result).IsInstanceOf<BadRequestResult>();
    }
}
