using Catalog.Application.Responses.Products;
using MediatR;

namespace Catalog.Application.Queries.Products
{
    public class GetAllProductsByBrandNameQuery : IRequest<IList<ProductsResponseDTO>>
    {
        public string BrandName { get; set; }

    }

}
