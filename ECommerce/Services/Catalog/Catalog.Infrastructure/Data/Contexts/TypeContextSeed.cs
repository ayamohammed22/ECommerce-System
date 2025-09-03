using Catalog.Core.Entities;
using MongoDB.Driver;
using System.Text.Json;

namespace Catalog.Infrastructure.Data.Contexts
{
    public static class TypeContextSeed
    {

        public static async Task seedDataAsync(IMongoCollection<ProductType> TypesCollection)
        {
            var hasType = await TypesCollection.Find(_ => true).AnyAsync();
            if (hasType)
                return;
            var path = Path.Combine("Data", "SeedData", "ProductType.json");
            if (!File.Exists(path))
            {
                Console.WriteLine($"file of seed data doesn't exist{path}");
                return;
            }
            var TypeData = await File.ReadAllTextAsync(path);
            var Types = JsonSerializer.Deserialize<List<ProductType>>(TypeData);
            if (Types?.Any() is true)
                await TypesCollection.InsertManyAsync(Types);

        }

    }
}
