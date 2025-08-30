using AutoMapper;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Handlers.Queries
{
    public class GetAllTypesQueryHandler : IRequestHandler<GetAllTypesQuery, IList<TypesResponseDTO>>
    {
        private readonly IProductTypeRepository _productTypeRepository;
        private readonly IMapper _mapper;

        public GetAllTypesQueryHandler(IProductTypeRepository productTypeRepository, IMapper mapper)
        {
            _productTypeRepository = productTypeRepository;
            _mapper = mapper;
        }

        public async Task<IList<TypesResponseDTO>> Handle(GetAllTypesQuery request, CancellationToken cancellationToken)
        {
            var types = await _productTypeRepository.GetAllProductTypes();
            var typesResponse = _mapper.Map<IList<ProductType>, IList<TypesResponseDTO>>(types.ToList());
            return typesResponse;
        }
    }
}
