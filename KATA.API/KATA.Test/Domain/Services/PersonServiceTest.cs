using KATA.Domain.Interfaces.Repositories;
using KATA.Domain.Models;
using KATA.Domain.Services;
using NFluent;
using NSubstitute;

namespace KATA.Test.Domain.Services;

public class PersonServiceTest
{
    [Fact]
    public async Task Should_Get_AllPersons()
    {
        //Arrange
        var listPersons = new List<Person>
        {
            new Person { Id = 8,FirstName = "Salma", LastName = "BOUARAIS" },
            new Person {Id = 9 , FirstName = "Kevin" , LastName = "Sousselier"}
        };
        IPersonRepository personRepository = Substitute.For<IPersonRepository>();
        personRepository.GetAllPersonsAsync().Returns(listPersons);
        var personService = new PersonService(personRepository);

        //Act
        var allPersons = await (personService.GetAllPersonsAsync());  
        
        //Assert
        Check.That(allPersons).HasSize(2);
    }


    [Fact]
    public async Task Should_Get_PersonById()
    {
        //Arrange
        var person = new Person { Id = 8,FirstName = "Salma", LastName = "BOUARAIS" };
        IPersonRepository personRepository = Substitute.For<IPersonRepository>();
        personRepository.GetPersonByIdAsync(person.Id).Returns(person);
        var personService = new PersonService(personRepository);

        //Act
        var result = await (personService.GetPersonByIdAsync(person.Id));

        //Assert
        Check.That(result).IsNotNull();
        Check.That(result.FirstName).Equals("Salma");
        Check.That(result.LastName).Equals("BOUARAIS");
    }
}
