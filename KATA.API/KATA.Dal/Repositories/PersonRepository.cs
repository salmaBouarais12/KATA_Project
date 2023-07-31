using KATA.Domain.Interfaces.Repositories;
using KATA.Domain.Models;
using Microsoft.EntityFrameworkCore;

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

    public async Task<IEnumerable<Person>> GetPersonByIdAsync(int id)
    {
        return (await _dbKataContext.People.ToListAsync()).Select(p => new Person
        {
            FirstName = p.FirstName,
            Id = p.Id,
            LastName = p.LastName
        }).Where(p => p.Id.Equals(id));
    }
}
