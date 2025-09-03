using Catalog.Core.Entities;
using MongoDB.Driver;
using System.Text.Json;

namespace Catalog.Infrastructure.Data.Contexts
{
    public static class CatalogContextSeed
    {
        public static async Task seedDataAsync(IMongoCollection<Product> ProductsCollection)
        {
            var hasProduct = await ProductsCollection.Find(_ => true).AnyAsync();
            if (hasProduct)
                return;
            var path = Path.Combine("Data", "SeedData", "Product.json");
            if (!File.Exists(path))
            {
                Console.WriteLine($"file of seed data doesn't exist{path}");
                return;
            }
            var ProductData = await File.ReadAllTextAsync(path);
            var Products = JsonSerializer.Deserialize<List<Product>>(ProductData);
            if (Products?.Any() is true)
                await ProductsCollection.InsertManyAsync(Products);

        }
    }
}
