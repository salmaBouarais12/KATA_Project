using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KATA.Domain.Interfaces.Sevices;

public interface IValueService
{
    Task<IEnumerable<string>> GetAllValuesAsync();
}
