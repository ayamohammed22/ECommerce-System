using Discount.GRPC.Protos;

namespace Basket.Application.GRPCServices
{
    public class DiscountGRPCServices
    {
        private readonly DiscountProtoService.DiscountProtoServiceClient _discountClient;

        public DiscountGRPCServices(DiscountProtoService.DiscountProtoServiceClient discountClient)
        {
            _discountClient = discountClient;
        }

        public async Task<CouponModel> GetDiscount(string productName)
        {
            var request = new GetDiscountRequest { ProductName = productName };
            return await _discountClient.GetDiscountAsync(request);
        }

    }
}
