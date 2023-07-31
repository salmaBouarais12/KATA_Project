using KATA.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace KATA.Dal.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly DbKataContext _dbKataContext;
        public PersonRepository(DbKataContext dbKataContext)
        {
            _dbKataContext = dbKataContext;
        }

        public async Task<IEnumerable<KATA.Domain.Models.Person>> GetAllPersonsAsync()
        {
            return (await _dbKataContext.People.ToListAsync()).Select(p => new KATA.Domain.Models.Person
            {
                FirstName = p.FirstName,
                Id = p.Id,
                LastName = p.LastName
            });
        }
    }
}
