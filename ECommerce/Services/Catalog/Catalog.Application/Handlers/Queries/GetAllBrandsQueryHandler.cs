using AutoMapper;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Handlers.Queries
{
    public class GetAllBrandsQueryHandler : IRequestHandler<GetAllBrandsQuery, IList<BrandsResponseDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IProductBrandRepository _productBrandRepository;

        public GetAllBrandsQueryHandler (IMapper mapper , IProductBrandRepository productBrandRepository) 
        {
           _mapper = mapper;
           _productBrandRepository = productBrandRepository;
        }
        public async Task<IList<BrandsResponseDTO>> Handle(GetAllBrandsQuery request, CancellationToken cancellationToken)
        {
            var brands =await  _productBrandRepository.GetAllProductBrands();
            var BrandsResponse = _mapper.Map<IList<ProductBrand>, IList<BrandsResponseDTO>>(brands.ToList());
            return BrandsResponse;
        }
    }
}
