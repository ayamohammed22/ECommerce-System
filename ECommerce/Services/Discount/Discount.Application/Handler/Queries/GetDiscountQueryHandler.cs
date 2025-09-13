using AutoMapper;
using Discount.Application.Queries;
using Discount.Core.Repositories;
using Discount.GRPC.Protos;
using Grpc.Core;
using MediatR;

namespace Discount.Application.Handler.Queries
{
    public class GetDiscountQueryHandler : IRequestHandler<GetDiscountQuery, CouponModel>
    {
        private readonly IDiscountRepo _discountRepo;
        private readonly IMapper _mapper;

        public GetDiscountQueryHandler(IDiscountRepo discountRepo, IMapper mapper)
        {
            _discountRepo = discountRepo;
            _mapper = mapper;
        }
        public async Task<CouponModel> Handle(GetDiscountQuery request, CancellationToken cancellationToken)
        {
            var coupon = await _discountRepo.GetDiscount(request.ProductName);
            if (coupon == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "Discount for this product not found"));
            }
            var couponModel = _mapper.Map<CouponModel>(coupon);
            return couponModel;
        }
    }
}
