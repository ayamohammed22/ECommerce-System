using Catalog.Core.Entities;

namespace Catalog.Core.Repositories
{
    public interface IProductBrandRepository
    {
        Task<IEnumerable<ProductBrand>> GetAllProductBrands();
    }
}
