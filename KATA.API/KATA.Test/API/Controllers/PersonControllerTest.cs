using KATA.API.Controllers;
using KATA.API.DTO.Responses;
using KATA.Domain.Interfaces.Sevices;
using KATA.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using NFluent;
using NSubstitute;

namespace KATA.Test.API.Controllers;

public class PersonControllerTest
{
    [Fact]
    public async Task Should_Get_PersonById()
    {
        //Arrange
        var Person = new Person { Id = 8, FirstName = "Salma", LastName = "BOUARAIS" };
        IPersonService personService = Substitute.For<IPersonService>();
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
        IPersonService personService = Substitute.For<IPersonService>();

        var personController = new PersonController(personService);
        var result = await personController.Get(default);

        Check.That(result).IsNotNull();
        Check.That(result).IsInstanceOf<NotFoundResult>();
    }
}
