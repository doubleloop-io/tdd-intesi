using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;

namespace BirthdayGreetingsKata.Infrastructure
{
    public class MsSqlEmployeeRepository
    {
        readonly string databaseConnectionString;

        public MsSqlEmployeeRepository(string databaseConnectionString)
        {
            this.databaseConnectionString = databaseConnectionString;
        }

        public async Task<IEnumerable<Employee>> Load()
        {
            await using var connection = new SqlConnection(databaseConnectionString);
            connection.Open();
            return await connection.QueryAsync<Employee>("select FirstName, LastName, Email, DateOfBirth from Employee");
        }
    }
}
