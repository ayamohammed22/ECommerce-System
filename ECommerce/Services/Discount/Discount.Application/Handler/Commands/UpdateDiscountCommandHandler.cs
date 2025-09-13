using AutoMapper;
using Discount.Application.Commands;
using Discount.Core.Entities;
using Discount.Core.Repositories;
using Discount.GRPC.Protos;
using MediatR;

namespace Discount.Application.Handler.Commands
{
    public class UpdateDiscountCommandHandler : IRequestHandler<UpdateDiscountCommand, CouponModel>
    {
        private readonly IDiscountRepo _discountRepo;
        private readonly IMapper _mapper;

        public UpdateDiscountCommandHandler(IDiscountRepo discountRepo, IMapper mapper)
        {
            _discountRepo = discountRepo;
            _mapper = mapper;
        }

        public async Task<CouponModel> Handle(UpdateDiscountCommand request, CancellationToken cancellationToken)
        {
            var coupon = _mapper.Map<Coupon>(request);
            await _discountRepo.UpdateDiscount(coupon);
            var couponModel = _mapper.Map<CouponModel>(coupon);
            return couponModel;
        }
    }
}
