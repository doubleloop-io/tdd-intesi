using System;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using BirthdayGreetingsKata.Infrastructure;
using Dapper;
using DotNet.Testcontainers.Containers.Builders;
using DotNet.Testcontainers.Containers.Configurations.Databases;
using DotNet.Testcontainers.Containers.Modules.Databases;
using FluentMigrator;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace BirthdayGreetingsKata.Tests
{
    public class MsSqlEmployeeRepositoryTests : IAsyncLifetime
    {
        MsSqlTestcontainer database;

        public async Task InitializeAsync()
        {
            var databaseBuilder = new TestcontainersBuilder<MsSqlTestcontainer>()
                .WithDatabase(
                    new MsSqlTestcontainerConfiguration
                    {
                        Password = "yourStrong(!)Password", // See following password policy: https://hub.docker.com/r/microsoft/mssql-server-linux/
                    });

            database = databaseBuilder.Build();
            await database.StartAsync();

            // Put the database update into a scope to ensure
            // that all resources will be disposed.
            using var scope = CreateServices(database.ConnectionString).CreateScope();
            scope.ServiceProvider.GetRequiredService<IMigrationRunner>().MigrateUp();
        }

        [Fact]
        public async Task NoEmployees()
        {
            await using var connection = new SqlConnection(database.ConnectionString);
            connection.Open();

            var result = await connection.QueryAsync<Employee>("select FirstName, LastName, Email, DateOfBirth from Employee");
            Assert.Empty(result);
        }

        [Fact]
        public async Task OneEmployee()
        {
            await using var connection = new SqlConnection(database.ConnectionString);
            connection.Open();

            var count = await connection.ExecuteAsync(@"insert Employee(FirstName, LastName, Email, DateOfBirth) values (@FirstName, @LastName, @Email, @DateOfBirth)", new[]
            {
                new {FirstName = "foo", LastName = "bar", Email = "a@b.com", DateOfBirth = "2021/05/19".ToDate(), },
            });
            Assert.Equal(1, count);

            var repository = new MsSqlEmployeeRepository(database.ConnectionString);

            var result = await repository.Load();
            Assert.Single(result);
        }

        public async Task DisposeAsync() =>
            await database.DisposeAsync();

        static IServiceProvider CreateServices(string connectionString) =>
            new ServiceCollection()
                .AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    .AddSqlServer()
                    .WithGlobalConnectionString(connectionString)
                    .ScanIn(typeof(DefineEmployeeTable).Assembly).For.Migrations())
                .BuildServiceProvider(false);
    }

    [Migration(1)]
    public class DefineEmployeeTable : Migration
    {
        public override void Up()
        {
            Create.Table("Employee")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("FirstName").AsString()
                .WithColumn("LastName").AsString()
                .WithColumn("Email").AsString()
                .WithColumn("DateOfBirth").AsDate();
        }

        public override void Down()
        {
        }
    }
}
