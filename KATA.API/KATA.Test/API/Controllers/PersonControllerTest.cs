using KATA.API.Controllers;
using KATA.API.DTO.Requests;
using KATA.API.DTO.Responses;
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
        var person = new Person { Id = 8, FirstName = "Salma", LastName = "BOUARAIS" };
        personService.GetPersonByIdAsync(Arg.Is<int>(x => x == person.Id)).Returns(person);
        //personService.GetPersonByIdAsync(Arg.Any<int>()).Returns(Person);

        var personController = new PersonController(personService);
        var personById = await personController.Get(person.Id);
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

    [Fact]
    public async Task Should_Not_Update_Person_And_Return_404_When_Person_Doesnt_Exist()
    {
        var person = new PostPersonRequest { FirstName = "Salma", LastName = "BOUARAIS" };

        var personController = new PersonController(personService);
        var result = await personController.Put(8, person);

        Check.That(result).IsNotNull();
        Check.That(result).IsInstanceOf<NotFoundResult>();
    }

    [Fact]
    public async Task Should_Update_Person()
    {
        var person = new PostPersonRequest { FirstName = "Lionel", LastName = "UpdateMessi" };
        //personService.UpdatePersonsAsync(2, Arg.Any<Person>()).Returns(new Person());
        personService.UpdatePersonsAsync(default, default!).ReturnsForAnyArgs(new Person());
        var personController = new PersonController(personService);
        var result = await personController.Put(2, person);

        Check.That(result).IsNotNull();
        Check.That(((ObjectResult)result).StatusCode).IsEqualTo(200);
    }

    [Fact]
    public async Task Should_Not_Delete_Person_And_Return_404_When_Person_Doesnt_Exist()
    {
        var personController = new PersonController(personService);
        var result = await personController.Delete(15);
        Check.That(result).IsInstanceOf<NotFoundResult>();
    }

    [Fact]
    public async Task Should_Delete_Person()
    {
        var person = new Person { Id = 1020, FirstName = "Test1", LastName = "Test1" };
        personService.GetPersonByIdAsync(Arg.Is<int>(x => x == person.Id)).Returns(person);

        var personController = new PersonController(personService);
        var personToDeletd = await personController.Delete(person.Id);

        Check.That(personToDeletd).IsNotNull();
        Check.That(personToDeletd).IsInstanceOf<NotFoundResult>();
    }
}
