using Catalog.Core.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Catalog.Infrastructure.Data.Contexts
{
    public static class BrandContextSeed
    {

        public static async Task seedDataAsync(IMongoCollection<ProductBrand> BrandsCollection)
        {
            var hasBrand = await BrandsCollection.Find(_ => true).AnyAsync();
            if (hasBrand)
                return;
            var path = Path.Combine("Data", "SeedData", "ProductBrand.json");
            if (!File.Exists(path))
            {
                Console.WriteLine($"file of seed data doesn't exist{path}");
                return;
            }
            var brandData = await File.ReadAllTextAsync(path);
            var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandData);
            if (brands?.Any() is true)
                await BrandsCollection.InsertManyAsync(brands);

        }
    }
}
