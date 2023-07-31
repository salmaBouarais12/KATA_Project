using KATA.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KATA.Dal.Repositories;

public class ValueRepository : IValueRepository
{
    public IEnumerable<string> GetAllValues()
    {
        return new string[] { "value1", "value2" };
    }
}
