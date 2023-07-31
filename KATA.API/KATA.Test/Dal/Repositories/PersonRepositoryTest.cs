using KATA.Dal;
using KATA.Dal.Repositories;
using Microsoft.EntityFrameworkCore;
using NFluent;

namespace KATA.Test.Dal.Repositories;

public class PersonRepositoryTest
{
    [Fact]
    public async Task Should_Get_Persons()
    {
        var options = new DbContextOptionsBuilder<DbKataContext>()
            .UseInMemoryDatabase("when_requesting_persons_on_repository")
            .Options;

        var fakePersons = new[]
        {
            new PersonEntity { Id = 1, FirstName = "FirstName", LastName = "LastName" }
        };

        await using (var ctx = new DbKataContext(options))
        {
            ctx.People.AddRange(fakePersons);
            await ctx.SaveChangesAsync();
        }

        await using (var ctx = new DbKataContext(options))
        {
            var personRepository = new PersonRepository(ctx);
            var persons = await personRepository.GetAllPersonsAsync();

            Check.That(persons).HasSize(1);
        }
    }
}