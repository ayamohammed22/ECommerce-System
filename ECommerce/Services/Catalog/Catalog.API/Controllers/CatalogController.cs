using Catalog.Application.Commands;
using Catalog.Application.Queries;
using Catalog.Application.Queries.Products;
using Catalog.Application.Responses;
using Catalog.Application.Responses.Products;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Catalog.API.Controllers
{
    public class CatalogController : BaseController
    {
        private readonly IMediator _mediator;

        public CatalogController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetProductById")]
        [ProducesResponseType(typeof(ProductResponseDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ProductResponseDTO>> GetProductById([FromQuery] GetProductByIdQuery query)
        {
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpGet("GetProductsByBrandName")]
        [ProducesResponseType(typeof(List<ProductsResponseDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<ProductsResponseDTO>>> GetProductsByBrandName([FromQuery] GetAllProductsByBrandNameQuery query)
        {
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpGet("GetProductsByProductName")]
        [ProducesResponseType(typeof(List<ProductsResponseDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<ProductsResponseDTO>>> GetProductsByProductName([FromQuery] GetProductsByNameQuery query)
        {
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpGet("GetAllProducts")]
        [ProducesResponseType(typeof(List<ProductsResponseDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<ProductsResponseDTO>>> GetProducts([FromQuery]GetAllProductsQuery query)
        {
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpPost("CreateProduct")]
        [ProducesResponseType(typeof(ProductResponseDTO), (int)HttpStatusCode.Created)]
        public async Task<ActionResult<ProductResponseDTO>> CreateProduct([FromBody] CreateProductCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPut("UpdateProduct")]
        public async Task<ActionResult<bool>> UpdateProduct([FromBody] UpdateProductCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete("DeleteProduct")]
        public async Task<ActionResult<bool>> DeleteProduct([FromQuery] DeleteProductCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpGet("GetAllBrands")]
        [ProducesResponseType(typeof(List<BrandsResponseDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<BrandsResponseDTO>>> GetBrands()
        {
            var response = await _mediator.Send(new GetAllBrandsQuery());
            return Ok(response);
        }

        [HttpGet("GetAllTypes")]
        [ProducesResponseType(typeof(List<TypesResponseDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<TypesResponseDTO>>> GetTypes()
        {
            var response = await _mediator.Send(new GetAllTypesQuery());
            return Ok(response);
        }
    }
}
