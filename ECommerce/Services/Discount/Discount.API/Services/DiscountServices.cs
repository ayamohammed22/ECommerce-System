using Discount.Application.Commands;
using Discount.Application.Queries;
using Discount.GRPC.Protos;
using Grpc.Core;
using MediatR;

namespace Discount.API.Services
{
    public class DiscountServices : DiscountProtoService.DiscountProtoServiceBase
    {
        private readonly IMediator _mediator;

        public DiscountServices(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var query = new GetDiscountQuery() { ProductName = request.ProductName };
            var response = await _mediator.Send(query);
            return response;
        }

        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var command = new CreateDiscountCommand()
            {
                ProductName = request.Coupon.ProductName,
                Description = request.Coupon.Description,
                Amount = request.Coupon.Amount
            };
            var response = await _mediator.Send(command);
            return response;
        }

        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var command = new UpdateDiscountCommand()
            {
                Id = request.Coupon.Id,
                ProductName = request.Coupon.ProductName,
                Description = request.Coupon.Description,
                Amount = request.Coupon.Amount
            };
            var response = await _mediator.Send(command);
            return response;
        }

        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var command = new DeleteDiscountCommand() { ProductName = request.ProductName };
            var response = await _mediator.Send(command);
            return new DeleteDiscountResponse() { Success = response };
        }
    }
}
