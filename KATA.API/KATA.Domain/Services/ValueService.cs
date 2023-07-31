using KATA.Domain.Interfaces.Repositories;
using KATA.Domain.Interfaces.Sevices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KATA.Domain.Services;

public class ValueService : IValueService
{
    private readonly IValueRepository _valueRepository;

    public ValueService(IValueRepository valueRepository)
    {
        _valueRepository = valueRepository;
    }

    public IEnumerable<string> GetAllValues()
    {
        return _valueRepository.GetAllValues();
    }
}
