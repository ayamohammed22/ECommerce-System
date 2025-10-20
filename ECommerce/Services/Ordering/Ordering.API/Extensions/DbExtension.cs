using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Ordering.Infrastructure.Data;
using Polly;

namespace Ordering.API.Extensions
{
    public static class DbExtension
    {
        public static IHost MigrateDatabase<TContext>(this IHost host, Action<TContext, IServiceProvider> seeder) where TContext : OrderContext
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetService<TContext>();
                var logger = services.GetRequiredService<ILogger<TContext>>();
                try
                {
                    logger.LogInformation("Migrating database associated with context {DbContextName}", typeof(TContext).Name);
                    var retry = Policy.Handle<SqlException>()
                           .WaitAndRetry(
                              retryCount: 5,
                              sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                              onRetry: (exception, timeSpan, retryCount) =>
                              {
                                  logger.LogWarning(exception, "Exception {ExceptionType} with message {Message} detected on attempt {retryCount} of {retries}", exception.GetType().Name, exception.Message, retryCount, 5);
                              }
                            );
                    retry.Execute(() => CallSedder(seeder, context, services));
                    logger.LogInformation("Migrated database associated with context {DbContextName}", typeof(TContext).Name);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An error occurred while migrating the database used on context {DbContextName}", typeof(TContext).Name);
                }
            }
            return host;
        }

        private static void CallSedder<TContext>(Action<TContext, IServiceProvider> seeder, TContext? context, IServiceProvider services) where TContext : OrderContext
        {
            // Apply any pending migrations
            context.Database.Migrate();
            // Seed the database
            seeder(context, services);
        }
    }
}
