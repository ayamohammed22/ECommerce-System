using Catalog.Application.Responses.Products;
using MediatR;

namespace Catalog.Application.Queries.Products
{
    public class GetProductsByNameQuery : IRequest<IList<ProductsResponseDTO>>
    {
        public string Name { get; set; }
    }
}
