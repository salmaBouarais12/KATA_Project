﻿using KATA.Domain.Models;

namespace KATA.Domain.Interfaces.Sevices;

public interface IPersonService
{
    Task<IEnumerable<Person>> GetAllPersonsAsync();
    Task<Person?> GetPersonByIdAsync(int id);
    Task<Person> AddPersonsAsync(Person person);
    Task<Person?> UpdatePersonsAsync(int id, Person person);
    Task<Person?> DeletePersonsAsync(int id);
}
