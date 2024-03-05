using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace EnumBugReproducer.EnumBugReproducer
{
  public static class EnumAwareDbMigrator
  {

    /// <summary>
    /// migrates db and reloads type so that enums are handled correctly after migration, c.f. https://www.npgsql.org/efcore/mapping/enum.html?tabs=without-datasource
    /// </summary>
    /// <returns></returns>
    public static async Task MigrateDbAndReloadTypes(DbContext context)
    {
      await context.Database.MigrateAsync();
      if (context.Database.GetDbConnection() is NpgsqlConnection npgsqlConnection) {
        await npgsqlConnection.OpenAsync();
        try {
          npgsqlConnection.ReloadTypes();
        } finally {
          await npgsqlConnection.CloseAsync();
        }
      }
    }
  }
}
