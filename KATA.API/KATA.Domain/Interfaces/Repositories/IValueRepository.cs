namespace KATA.Domain.Interfaces.Repositories;

public interface IValueRepository
{
    Task<IEnumerable<string>> GetAllValuesAsync();
}
