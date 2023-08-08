using KATA.Domain.Interfaces.Repositories;
using KATA.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
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
        var result = await _dbKataContext.People.SingleOrDefaultAsync(p => p.Id == id);
        if (result is not null)
        {
            return new Person
            {
                FirstName = result.FirstName,
                Id = result.Id,
                LastName = result.LastName
            };
        }
        return null;
    }

    public async Task<Person> AddPersonsAsync(Person person)
    {
        var personToAdd = new PersonEntity { FirstName = person.FirstName, LastName = person.LastName };
        _dbKataContext.People.Add(personToAdd);
        await _dbKataContext.SaveChangesAsync();
        return new Person { Id = personToAdd.Id, FirstName = personToAdd.FirstName,LastName = personToAdd.LastName };
    }

    public async Task<Person> UpdatePersonsAsync(int id, Person person)
    {
        var personToFind = await _dbKataContext.People.FindAsync(id);
        var personTobeUpdated = new PersonEntity { FirstName = person.FirstName, LastName = person.LastName };
        if (personToFind == null)
        {
            return null;
        }
        personToFind.FirstName = personTobeUpdated.FirstName;
        personToFind.LastName = personTobeUpdated.LastName;
        await _dbKataContext.SaveChangesAsync();
        return new Person { Id = personToFind.Id, FirstName = personToFind.FirstName, LastName = personToFind.LastName };
    }

    public async Task<Person> DeletePersonsAsync(int id)
    {
        var personToFind = await _dbKataContext.People.FindAsync(id);
        if (personToFind == null)
        {
            return null;
        }
        _dbKataContext.People.Remove(personToFind);
        await _dbKataContext.SaveChangesAsync();
        return new Person { Id = personToFind.Id, FirstName = personToFind.FirstName, LastName = personToFind.LastName };
    }
}
