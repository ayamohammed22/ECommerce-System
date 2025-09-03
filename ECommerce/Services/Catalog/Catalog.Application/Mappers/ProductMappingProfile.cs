using AutoMapper;
using Catalog.Application.Commands;
using Catalog.Application.Responses;
using Catalog.Application.Responses.Products;
using Catalog.Core.Entities;
using Catalog.Core.Specification;

namespace Catalog.Application.Mappers
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            // Brand
            CreateMap<ProductBrand, BrandsResponseDTO>();

            // Product
            CreateMap<Product, ProductsResponseDTO>();
            CreateMap<Pagination<Product>, Pagination<ProductsResponseDTO>>();
            CreateMap<Product, ProductResponseDTO>().ReverseMap();
            CreateMap<CreateProductCommand, Product>();
            CreateMap<UpdateProductCommand, Product>();

            // Types
            CreateMap<ProductType, TypesResponseDTO>();
        }
    }
}
