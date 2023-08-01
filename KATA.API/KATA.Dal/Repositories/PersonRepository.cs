using KATA.Domain.Interfaces.Repositories;
using KATA.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace KATA.Dal.Repositories;

public class PersonRepository : IPersonRepository
{
    private readonly DbKataContext _dbKataContext;
    public PersonRepository(DbKataContext dbKataContext)
    {
        _dbKataContext = dbKataContext;
    }

    public async Task<IEnumerable<Person>> GetAllPersonsAsync()
    {
        return (await _dbKataContext.People.ToListAsync()).Select(p => new Person
        {
            FirstName = p.FirstName,
            Id = p.Id,
            LastName = p.LastName
        });
    }

    public async Task<Person?> GetPersonByIdAsync(int id)
    {
        var result =  await _dbKataContext.People.SingleOrDefaultAsync(p => p.Id == id);
        if (result is not null) {
            return new Person
            {
                FirstName = result.FirstName,
                Id = result.Id,
                LastName = result.LastName
            };
        }
        return null;
    }
}
