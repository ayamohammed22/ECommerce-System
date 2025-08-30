using Catalog.Application.Responses.Products;
using Catalog.Core.Specification;
using MediatR;

namespace Catalog.Application.Queries.Products
{
    public class GetAllProductsQuery : IRequest<Pagination<ProductsResponseDTO>>
    {
        public CatalogSpecParms catalogSpecParms { get; set; }
    }
}
