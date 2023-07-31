namespace KATA.Domain.Interfaces.Sevices;

public interface IValueService
{
    Task<IEnumerable<string>> GetAllValuesAsync();
}
