﻿using Discount.GRPC.Protos;
using MediatR;

namespace Discount.Application.Queries
{
    public class GetDiscountQuery : IRequest<CouponModel>
    {
        public string ProductName { get; set; }
    }
}
