using KATA.Domain.Models;

namespace KATA.Domain.Interfaces.Sevices;

public interface IPersonService
{
    Task<IEnumerable<Person>> GetAllPersonsAsync();
    Task<IEnumerable<Person>> GetPersonByIdAsync(int id);
}
