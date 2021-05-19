using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BirthdayGreetingsKata.Infrastructure
{
    public class CsvEmployeeRepository : IEmployeeRepository
    {
        readonly string file;

        public CsvEmployeeRepository(string file)
        {
            this.file = file;
        }

        public Task<IEnumerable<Employee>> Load() =>
            File.Exists(file) ? ParseFile() : NoFile();

        static Task<IEnumerable<Employee>> NoFile() =>
            Task.FromResult(Enumerable.Empty<Employee>());

        async Task<IEnumerable<Employee>> ParseFile()
        {
            var lines = await File.ReadAllLinesAsync(file);
            return lines
                .Skip(1)
                .Select(ParseEmployee);
        }

        static Employee ParseEmployee(string single)
        {
            var parts = single.Split(new[] {','})
                .Select(x => x.Trim())
                .ToArray();

            return new Employee
            {
                LastName = parts[0],
                FirstName = parts[1],
                DateOfBirth = parts[2].ToDate(),
                Email = parts[3],
            };
        }
    }
}
