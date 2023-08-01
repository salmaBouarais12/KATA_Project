using KATA.Domain.Interfaces.Repositories;
using KATA.Domain.Interfaces.Sevices;

namespace KATA.Domain.Services;

public class ValueService : IValueService
{
    private readonly IValueRepository _valueRepository;

    public ValueService(IValueRepository valueRepository) =>
        _valueRepository = valueRepository;

    public async Task<IEnumerable<string>> GetAllValuesAsync()
    {
        return await _valueRepository.GetAllValuesAsync();
    }
}
