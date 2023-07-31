﻿namespace KATA.Domain.Interfaces.Repositories;

public interface IPersonRepository
{
    Task<IEnumerable<KATA.Domain.Models.Person>> GetAllPersonsAsync();
}
