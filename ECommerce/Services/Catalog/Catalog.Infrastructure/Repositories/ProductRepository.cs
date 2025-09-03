using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using Catalog.Core.Specification;
using Catalog.Infrastructure.Data.Contexts;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository, IProductBrandRepository, IProductTypeRepository
    {
        private readonly ICatalogContext _context;

        public ProductRepository(ICatalogContext context)
        {
            _context = context;
        }

        public async Task<Product> GetProductById(string Id)
        {
            var product = await _context.Products.Find(p => p.Id == Id).FirstOrDefaultAsync();
            return product;
        }
        public async Task<Pagination<Product>> GetAllProducts(CatalogSpecParms catalogSpecParms)
        {
            var builder = Builders<Product>.Filter;
            var filter = builder.Empty;
            if (!string.IsNullOrEmpty(catalogSpecParms.Search))
            {
                filter &= builder.Where(p => p.Name.ToLower().Contains(catalogSpecParms.Search.ToLower()));
            }
            if (!string.IsNullOrEmpty(catalogSpecParms.BrandId))
            {
                filter &= builder.Eq(p => p.Brand.Id, catalogSpecParms.BrandId);
            }
            if (!string.IsNullOrEmpty(catalogSpecParms.TypeId))
            {
                filter &= builder.Eq(p => p.Type.Id, catalogSpecParms.TypeId);
            }
            var totalItems = await _context.Products.CountDocumentsAsync(filter);
            var data = await DataFilter(catalogSpecParms, filter);
            return new Pagination<Product>(catalogSpecParms.PageIndex, catalogSpecParms.PageSize, (int)totalItems, data);

        }
        public async Task<IEnumerable<Product>> GetAllProductsByBrand(string Name)
        {
            return await _context.Products.Find(p => p.Brand.Name == Name).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetAllProductsByName(string Name)
        {
            return await _context.Products.Find(p => p.Name == Name).ToListAsync();
        }


        public async Task<Product> CreateProduct(Product product)
        {
            await _context.Products.InsertOneAsync(product);
            return product;
        }

        public async Task<bool> DeleteProduct(string Id)
        {
            var deleted = await _context.Products.DeleteOneAsync(p => p.Id == Id);
            return deleted.IsAcknowledged && deleted.DeletedCount > 0;
        }
        public async Task<bool> UpdateProduct(Product product)
        {
            var updated = await _context.Products.ReplaceOneAsync(p => p.Id == product.Id, product);
            return updated.IsAcknowledged && updated.ModifiedCount > 0;
        }

        public async Task<IEnumerable<ProductBrand>> GetAllProductBrands()
        {
            return await _context.Brands.Find(b => true).ToListAsync();
        }



        public async Task<IEnumerable<ProductType>> GetAllProductTypes()
        {
            return await _context.Types.Find(t => true).ToListAsync();
        }

        private async Task<IReadOnlyList<Product>> DataFilter(CatalogSpecParms catalogSpecParms, FilterDefinition<Product> filter)
        {
            var SortDif = Builders<Product>.Sort.Ascending(p => p.Name);
            if (!string.IsNullOrEmpty(catalogSpecParms.SortBy))
            {
                switch (catalogSpecParms.SortBy)
                {
                    case "Price Asc":
                        SortDif = Builders<Product>.Sort.Ascending(p => p.price);
                        break;
                    case "Price Dsc":
                        SortDif = Builders<Product>.Sort.Descending(p => p.price);
                        break;
                    default:
                        SortDif = Builders<Product>.Sort.Descending(p => p.Name);
                        break;

                }
            }
            return await _context.Products.Find(filter).Sort(SortDif)
                .Skip(catalogSpecParms.PageSize * (catalogSpecParms.PageIndex - 1))
                .Limit(catalogSpecParms.PageSize)
                .ToListAsync();
        }
    }
}
