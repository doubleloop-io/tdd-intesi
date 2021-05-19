using System.Collections.Generic;
using System.Threading.Tasks;

namespace BirthdayGreetingsKata
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> Load();
    }
}