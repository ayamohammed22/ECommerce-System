using AutoMapper;
using Catalog.Application.Queries.Products;
using Catalog.Application.Responses.Products;
using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Handlers.Queries.Products
{
    public class GetAllProductByBrandNameQueryHandler : IRequestHandler<GetAllProductsByBrandNameQuery, IList<ProductsResponseDTO>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetAllProductByBrandNameQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<IList<ProductsResponseDTO>> Handle(GetAllProductsByBrandNameQuery request, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetAllProductsByBrand(request.BrandName);
            var productsResponse = _mapper.Map<IList<Product>, IList<ProductsResponseDTO>>(products.ToList());
            return productsResponse;
        }
    }
}
