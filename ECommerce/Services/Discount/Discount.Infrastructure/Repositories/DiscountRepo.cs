using Dapper;
using Discount.Core.Entities;
using Discount.Core.Repositories;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Discount.Infrastructure.Repositories
{
    public class DiscountRepo : IDiscountRepo
    {
        private readonly IConfiguration _configuration;

        public DiscountRepo(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<Coupon> GetDiscount(string productName)
        {
            await using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DataBaseSettings:ConnectionString"));
            var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>
                  ("SELECT * FROM Coupon WHERE ProductName = @productName",
                  new
                  {
                      productName = productName,
                  });
            if (coupon == null)
                return new Coupon() { Amount = 0, Description = "No Discount for this product", ProductName = productName };
            return coupon;
        }
        public async Task<bool> CreateDiscount(Coupon coupon)
        {
            await using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DataBaseSettings:ConnectionString"));
            var created = await connection.ExecuteAsync
                ("INSERT INTO Coupon (ProductName,Amount,Description) VALUES (@ProductName,@Amount,@Description)",
                new
                {
                    ProductName = coupon.ProductName,
                    Amount = coupon.Amount,
                    Description = coupon.Description,
                });
            if (created == 1) return true;
            return false;
        }

        public async Task<bool> UpdateDiscount(Coupon coupon)
        {
            await using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DataBaseSettings:ConnectionString"));
            var updated = await connection.ExecuteAsync
              ("UPDATE Coupon SET ProductName = @ProductName,Amount = @Amount ,Description = @Description) WHERE Id = @Id",
              new
              {
                  Id = coupon.Id,
                  ProductName = coupon.ProductName,
                  Amount = coupon.Amount,
                  Description = coupon.Description,
              });
            if (updated == 1) return true;
            return false;
        }
        public async Task<bool> DeleteDiscount(string productName)
        {
            await using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DataBaseSettings:ConnectionString"));
            var deleted = await connection.ExecuteAsync
                    ("DELETE FROM Coupon where ProductName = @ProductName",
                    new
                    {
                        ProductName = productName
                    });
            if (deleted == 1) return true;
            return false;
        }

    }
}
