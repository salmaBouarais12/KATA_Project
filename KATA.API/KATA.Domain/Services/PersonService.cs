using KATA.Domain.Interfaces.Repositories;
using KATA.Domain.Interfaces.Sevices;
using KATA.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KATA.Domain.Services
{
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
    }
}
