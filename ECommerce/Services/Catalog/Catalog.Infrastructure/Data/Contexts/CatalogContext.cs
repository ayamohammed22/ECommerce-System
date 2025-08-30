using Catalog.Core.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Data.Contexts
{
    public class CatalogContext : ICatalogContext
    {
        public IMongoCollection<Product> Products { get; }

        public IMongoCollection<ProductBrand> Brands { get; }

        public IMongoCollection<ProductType> Types { get; }
        public CatalogContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration["DataBaseSettings:ConnectionString"]);
            var database = client.GetDatabase(configuration["DataBaseSettings:DataBaseName"]);
            Brands = database.GetCollection<ProductBrand>(configuration["DataBaseSettings:BrandsCollection"]);
            Types = database.GetCollection<ProductType>(configuration["DataBaseSettings:TypesCollection"]);
            Products = database.GetCollection<Product>(configuration["DataBaseSettings:ProductsCollection"]);
            _ = BrandContextSeed.seedDataAsync(Brands);
            _ = TypeContextSeed.seedDataAsync(Types);
            _ = CatalogContextSeed.seedDataAsync(Products);
        }


    }
}
