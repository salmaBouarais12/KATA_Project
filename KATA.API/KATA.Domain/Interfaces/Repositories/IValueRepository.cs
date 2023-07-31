using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KATA.Domain.Interfaces.Repositories;

public interface IValueRepository
{
    IEnumerable<string> GetAllValues();
}
