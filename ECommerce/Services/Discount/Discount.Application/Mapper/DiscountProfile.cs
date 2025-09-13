using AutoMapper;
using Discount.Application.Commands;
using Discount.Core.Entities;
using Discount.GRPC.Protos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discount.Application.Mapper
{
    public class DiscountProfile : Profile
    {
        public DiscountProfile() {
            CreateMap<Coupon, CouponModel>().ReverseMap();
            CreateMap<CreateDiscountCommand, Coupon>();
            CreateMap<UpdateDiscountCommand, Coupon>();

        }
    }
}
