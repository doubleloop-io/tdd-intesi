using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BirthdayGreetingsKata.Infrastructure
{
    public class CsvEmployeeRepository
    {
        readonly string file;

        public CsvEmployeeRepository(string file)
        {
            this.file = file;
        }

        public IEnumerable<Employee> Load() =>
            File.Exists(file) ? ParseFile() : NoFile();

        static IEnumerable<Employee> NoFile() =>
            Enumerable.Empty<Employee>();

        IEnumerable<Employee> ParseFile() =>
            File.ReadAllLines(file)
                .Skip(1)
                .Select(ParseEmployee);

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
