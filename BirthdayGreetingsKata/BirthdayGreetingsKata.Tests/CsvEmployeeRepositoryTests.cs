using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BirthdayGreetingsKata.Infrastructure;
using Xunit;

namespace BirthdayGreetingsKata.Tests
{
    /*
     * TEST LIST:
     * x file w/ 1 employee
     * x file w/out employees
     * x file w/ many employees
     * - missing columns
     * x missing file
     * - dati errati (data nascita nel futuro)
     * - dati malformattati
     * - potrebbero essere piu' file?
     * - l'header e' sempre presente?
     */
    public class CsvEmployeeRepositoryTests
    {
        [Fact]
        public async Task LoadOneEmployee()
        {
            // SETUP/CLEANUP EXTERNAL RESOURCE
            // creare un file
            var file = nameof(LoadOneEmployee);
            await File.WriteAllLinesAsync(
                file,
                new[]
                {
                    // con un header
                    "last_name, first_name, date_of_birth, email",
                    // con una riga
                    "foo, bar, 1982/11/08, a@b.com"
                }
            );
            // passare il path del file al repository
            var repo = new CsvEmployeeRepository(file);

            var result = await repo.Load();

            var employee = Assert.Single(result);
            Assert.Equal("foo", employee.LastName);
            Assert.Equal("bar", employee.FirstName);
            Assert.Equal("a@b.com", employee.Email);
            Assert.Equal("1982/11/08".ToDate(), employee.DateOfBirth);
            // TEARDOWN EXTERNAL RESOURCE
        }

        [Fact]
        public async Task LoadManyEmployees()
        {
            var file = nameof(LoadManyEmployees);
            File.WriteAllLines(
                file,
                new[]
                {
                    "last_name, first_name, date_of_birth, email",
                    "foo, bar, 1982/11/08, a@b.com",
                    "Doe, John, 1982/10/08, john.doe@foobar.com",
                }
            );
            var repo = new CsvEmployeeRepository(file);

            var result = await repo.Load();

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task NoEmployees()
        {
            var file = nameof(LoadManyEmployees);
            File.WriteAllLines(
                file,
                new[]
                {
                    "last_name, first_name, date_of_birth, email",
                }
            );
            var repo = new CsvEmployeeRepository(file);

            var result = await repo.Load();

            Assert.Empty(result);
        }

        [Fact]
        public async Task MissingFile()
        {
            var file = nameof(MissingFile);
            var repo = new CsvEmployeeRepository(file);

            var result = await repo.Load();

            Assert.Empty(result);
        }
    }
}
