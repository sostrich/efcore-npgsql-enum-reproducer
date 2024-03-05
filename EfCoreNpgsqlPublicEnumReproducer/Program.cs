

using EnumBugReproducer.DAL;
using EnumBugReproducer.Models.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Npgsql;
using System.Data;
using System.Threading.Tasks;


namespace EnumBugReproducer
{

    public static class EnumAwareDbMigrator
    {

        /// <summary>
        /// migrates db and reloads type so that enums are handled correctly after migration, c.f. https://www.npgsql.org/efcore/mapping/enum.html
        /// </summary>
        /// <returns></returns>
        public static async Task MigrateDbAndReloadTypes(DbContext context)
        {
            await context.Database.MigrateAsync();
            if (context.Database.GetDbConnection() is NpgsqlConnection npgsqlConnection)
            {
                if (npgsqlConnection.State == ConnectionState.Closed)
                {
                    await npgsqlConnection.OpenAsync();
                }
                try
                {
                    await npgsqlConnection.ReloadTypesAsync();
                }
                finally
                {
                    await npgsqlConnection.CloseAsync();
                }
            }
        }
    }

    public static class Program
    {
        private const string ConnString = "Server=127.0.0.1;Port=5432;Database=postgres;User Id=postgres;Password=password;";


        public static async Task Main(string[] args)
        {
            var dataSourceBuilder = new NpgsqlDataSourceBuilder(ConnString);
            dataSourceBuilder.MapEnum<MyEnum>();

            var host = new HostBuilderCreator().CreateHostBuilder(args).Build();
            await host.Services.GetRequiredService<MyContext>().Database.MigrateAsync();
            await EnumAwareDbMigrator.MigrateDbAndReloadTypes(host.Services.GetRequiredService<MyContext>());

        }

        public class HostBuilderCreator
        {
            public IHostBuilder CreateHostBuilder(string[] args = null)
            {
                ;

                var hostBuilder = Host.CreateDefaultBuilder(args)

                .ConfigureServices(AddDbContext<MyContext>);
                return hostBuilder;
            }
        }


        private static void AddDbContext<TDbContext>(IServiceCollection services) where TDbContext : DbContext
        {
            services.AddDbContext<TDbContext>((serviceProvider, options) =>
            {
                var connectionString = ConnString;
                options.UseNpgsql(connectionString);
            });
        }
    }
}