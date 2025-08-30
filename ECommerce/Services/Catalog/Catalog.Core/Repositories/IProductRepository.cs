using Catalog.Core.Entities;
using Catalog.Core.Specification;

namespace Catalog.Core.Repositories
{
    public interface IProductRepository
    {
        Task<Pagination<Product>> GetAllProducts(CatalogSpecParms catalogSpecParms);
        Task<Product> GetProductById(string Id);
        Task<IEnumerable<Product>> GetAllProductsByName(string Name);
        Task<IEnumerable<Product>> GetAllProductsByBrand(string Name);
        Task<Product> CreateProduct(Product product);
        Task<bool> UpdateProduct(Product product);
        Task<bool> DeleteProduct(string Id);
    }
}
