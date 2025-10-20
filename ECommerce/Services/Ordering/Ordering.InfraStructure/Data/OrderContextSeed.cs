using Microsoft.Extensions.Logging;
using Ordering.Core.Entities;

namespace Ordering.Infrastructure.Data
{
    public class OrderContextSeed
    {
        public static async Task SeedAsync(OrderContext orderContext, ILogger<OrderContextSeed> logger)
        {
            if (!orderContext.Orders.Any())
            {
                orderContext.Orders.AddRange(GetPreconfiguredOrders());
                await orderContext.SaveChangesAsync();
                logger.LogInformation("Seed database associated with context {DbContextName}", typeof(OrderContext).Name);
            }
        }
        private static IEnumerable<Order> GetPreconfiguredOrders()
        {
            return new List<Core.Entities.Order>
            {
                new Core.Entities.Order() { Username = "swn", FirstName = "Saeed", LastName = "Nasseri", EmailAddress = "swn@gmail.com",
                        AddressLine = "123 Main St", Country = "Iran", TotalPrice = 350, ZipCode = "11166",
                         CardName = "Visa", CardNumber ="132442483469", CVV="123",
                      Createby = "sysem", Expiration = "12/26", LastModifiedBy = "system",PaymentMethod = 1, LastModifiedDate = new DateTime() }
            };
        }
    }
}
