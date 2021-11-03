using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace Mary.Data
{
	public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
	{
		private Dictionary<string, Func<string, ApplicationDbContext>> DbCreateActions { get; set; }

		private string ConnectionStringName { get; set; } = "";
		private string DotNetCoreEnvironment { get; set; } = "ASPNETCORE_ENVIRONMENT";

		public ApplicationDbContextFactory()
		{
			DbCreateActions = new Dictionary<string, Func<string, ApplicationDbContext>>()
			{
                {"SqLite", CreateSqliteDbContext },
                {"SqlServer", CreateMsSqlDbContext }
            };
        }

		public ApplicationDbContext CreateDbContext(string[] args)
		{
			var configuration = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json")
				.AddJsonFile($"appsettings.Development.json", optional: true)
				.AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable(DotNetCoreEnvironment)}.json", optional: true)
				.AddEnvironmentVariables()
				.Build();

			var connectionType = configuration.GetSection($"ConnectionStrings:Type").Value;

			var connectionString = configuration.GetSection($"ConnectionStrings:{connectionType}:connectionString").Value;

			if (string.IsNullOrEmpty(connectionString))
				throw new ArgumentException($"Connection string '{ConnectionStringName}' is null or empty.", nameof(connectionString));

			Console.WriteLine($"DesignTimeDbContextFactoryBase.Create(string): Connection string: '{connectionString}'.");

			var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

			var dbcontext = new ApplicationDbContext((new DbContextOptionsBuilder<ApplicationDbContext>()).UseSqlServer(connectionString).Options);

			return DbCreateActions[connectionType].Invoke(connectionString);
		}

        private ApplicationDbContext CreateSqliteDbContext(string connectionString)
			=> new ApplicationDbContext((new DbContextOptionsBuilder<ApplicationDbContext>()).UseSqlite(new SqliteConnection((new SqliteConnectionStringBuilder { DataSource = connectionString }).ToString())).Options);

		private ApplicationDbContext CreateMsSqlDbContext(string connectionString)
			=> new ApplicationDbContext((new DbContextOptionsBuilder<ApplicationDbContext>()).UseSqlServer(connectionString).Options);
    }
}
