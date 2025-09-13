using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace Discount.Infrastructure.Extenstions
{
    public static class DBExtenstion
    {
        public static IHost MigrateDataBase<TContext>(this IHost host)
        {
            using (var scoped = host.Services.CreateScope())
            {
                var services = scoped.ServiceProvider;
                var config = services.GetRequiredService<IConfiguration>();
                var logger = services.GetRequiredService<ILogger<TContext>>();
                try
                {
                    logger.LogInformation("Discount DB migration start");
                    ApplyMigrations(config);
                    logger.LogInformation("Discount DB migration done");
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Can't Create Discount DB migration");
                    throw;
                }
            }

            return host;
        }

        private static void ApplyMigrations(IConfiguration config)
        {
            var retry = 5;
            while (retry-- > 0)
            {
                try
                {
                    using var connection = new NpgsqlConnection(config.GetValue<string>("DataBaseSettings:ConnectionString"));
                    connection.Open();
                    using var command = new NpgsqlCommand
                    {
                        Connection = connection,
                    };
                    command.CommandText = "DROP TABLE IF EXISTS Coupon";
                    command.ExecuteNonQuery();
                    command.CommandText = @"CREATE TABLE Coupon(Id SERIAL PRIMARY KEY,
                                                      ProductName VARCHAR(200) NOT NULL,
                                                       Description TEXT,
                                                       Amount INT)";
                    command.ExecuteNonQuery();
                    command.CommandText = "INSERT INTO Coupon(ProductName, Description, Amount) VALUES('iPhone 14 Pro','iPhone discount',200)";
                    command.ExecuteNonQuery();
                    command.CommandText = "INSERT INTO Coupon(ProductName, Description, Amount) VALUES('Galaxy Tab S8','Sumsung discount',400)";
                    command.ExecuteNonQuery();
                    break;
                }
                catch (Exception ex)
                {
                    if (retry == 0) throw;
                    Thread.Sleep(1000);
                }
            }
        }
    }
}
