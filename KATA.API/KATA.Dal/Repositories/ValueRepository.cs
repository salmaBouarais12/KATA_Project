using KATA.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace KATA.Dal.Repositories;

public class ValueRepository : IValueRepository
{
    private readonly DbKataContext _dbKataContext;
    public ValueRepository(DbKataContext dbKataContext)
    {
        _dbKataContext = dbKataContext;
    }

    public async Task<IEnumerable<string>> GetAllValuesAsync()
    {
        // return new string[] { "value1", "value2" };
        return await _dbKataContext.People.Select(p => p.FirstName).ToListAsync();
    }
}
