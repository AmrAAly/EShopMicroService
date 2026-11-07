using Discount.Grpc.Data;
using Discount.Grpc.Models;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Services
{
    public class DiscountService(DiscountDbContext dbContext, ILogger<DiscountService> logger) : DiscountProtoService.DiscountProtoServiceBase
    {
        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            if (request.Coupon is null)
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Coupon is null"));

            var coupon = request.Coupon.Adapt<Coupon>();
            dbContext.Coupons.Add(coupon);
            await dbContext.SaveChangesAsync();
            logger.LogInformation("Discount is added for ProductName : {ProductName}, Amount : {Amount}", coupon.ProductName, coupon.Amount);
            return coupon.Adapt<CouponModel>();

        }

        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var coupon = await dbContext.Coupons.FirstOrDefaultAsync(c => c.ProductName == request.ProductName);

            if (coupon is null)
                coupon = new Coupon
                {
                    ProductName = request.ProductName,
                    Description = "No Discount Desc",
                    Amount = 0
                };

            var couponModel = coupon.Adapt<CouponModel>();
            logger.LogInformation("Discount is retrieved for ProductName : {ProductName}, Amount : {Amount}", couponModel.ProductName, couponModel.Amount);
            return couponModel;
        }

        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            if (request.Coupon is null)
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Coupon is null"));
            
            var recored = await dbContext.Coupons.FirstOrDefaultAsync(c => c.Id == request.Coupon.Id);
            if (recored is null)
                throw new RpcException(new Status(StatusCode.NotFound, $"Discount with Id={request.Coupon.Id} is not found."));
            var coupon = request.Coupon.Adapt(recored);

            dbContext.Coupons.Update(coupon);
            await dbContext.SaveChangesAsync();
            logger.LogInformation("Discount is updated for ProductName : {ProductName}, Amount : {Amount}", coupon.ProductName, coupon.Amount);
            return coupon.Adapt<CouponModel>();
        }

        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var coupon = dbContext.Coupons.FirstOrDefault(c => c.ProductName == request.ProductName);
            if (coupon is null)
                throw new RpcException(new Status(StatusCode.NotFound, $"Discount with ProductName={request.ProductName} is not found."));

            dbContext.Coupons.Remove(coupon);
          
            await dbContext.SaveChangesAsync();
            logger.LogInformation("Discount is deleted for ProductName : {ProductName}, Amount : {Amount}", coupon.ProductName, coupon.Amount);
            return new DeleteDiscountResponse { Success = true };
        }
    }
}
