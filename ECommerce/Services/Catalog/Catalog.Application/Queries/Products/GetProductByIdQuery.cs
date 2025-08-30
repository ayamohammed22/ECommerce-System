using Catalog.Application.Responses.Products;
using MediatR;

namespace Catalog.Application.Queries.Products
{
    public class GetProductByIdQuery : IRequest<ProductResponseDTO>
    {
        public string Id { get; set; }

    }
}
