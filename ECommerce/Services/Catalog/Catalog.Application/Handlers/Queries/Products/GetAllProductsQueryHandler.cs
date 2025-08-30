using AutoMapper;
using Catalog.Application.Queries.Products;
using Catalog.Application.Responses.Products;
using Catalog.Core.Repositories;
using Catalog.Core.Specification;
using MediatR;

namespace Catalog.Application.Handlers.Queries.Products
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, Pagination<ProductsResponseDTO>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetAllProductsQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public async Task<Pagination<ProductsResponseDTO>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetAllProducts(request.catalogSpecParms);
            var productsResponse = _mapper.Map<Pagination<ProductsResponseDTO>>(products);
            return productsResponse;
        }
    }
}
