﻿using KATA.Domain.Interfaces.Repositories;
using KATA.Domain.Interfaces.Sevices;
using KATA.Domain.Models;

namespace KATA.Domain.Services;

public class PersonService : IPersonService
{
    private readonly IPersonRepository _personRepository;
    public PersonService(IPersonRepository personRepository)
    {
        _personRepository = personRepository;
    }

    public async Task<IEnumerable<Person>> GetAllPersonsAsync()
    {
        return await _personRepository.GetAllPersonsAsync();
    }

    public async Task<Person?> GetPersonByIdAsync(int id)
    {
        return await _personRepository.GetPersonByIdAsync(id);
    }
    public async Task<Person> AddPersonsAsync(Person person)
    {
        return await _personRepository.AddPersonsAsync(person);
    }

    public async Task<Person?> UpdatePersonsAsync(int id, Person person)
    {
        return await _personRepository.UpdatePersonsAsync(id,person);
    }

    public async Task<Person?> DeletePersonsAsync(int id)
    {
        return await _personRepository.DeletePersonsAsync(id);
    }
}
