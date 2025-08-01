using Discount.Grpc.Data;
using Discount.Grpc.Models;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Services;

public class DiscountService
    (DiscountContext dbContext, ILogger<DiscountService> logger) : DiscountProtoService.DiscountProtoServiceBase
{
    public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
        var coupon = request.Coupon.Adapt<Coupon>();
        if (coupon == null)
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request"));

        dbContext.Coupons.Add(coupon);
        await dbContext.SaveChangesAsync();

        logger.LogInformation("Discount is successfully created. ProductName : {productName}, Amount : {amount}", coupon.ProductName, coupon.Amount);

        return coupon.Adapt<CouponModel>();

    }

    public override async Task<CouponModel> UpdatetDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {
        var coupon = request.Coupon.Adapt<Coupon>();
        if (coupon == null)
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request"));

        dbContext.Coupons.Update(coupon);
        await dbContext.SaveChangesAsync();

        logger.LogInformation("Discount is successfully updated. ProductName : {productName}, Amount : {amount}", coupon.ProductName, coupon.Amount);

        return coupon.Adapt<CouponModel>();

    }

    public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
    {
        var coupon = await dbContext
            .Coupons
            .FirstOrDefaultAsync(c => c.ProductName.Equals(request.ProductName));
        if (coupon == null)
            throw new RpcException(new Status(StatusCode.NotFound, $"Disoucnt with ProductName={request.ProductName} does not exist!"));

        dbContext.Remove(coupon);
        await dbContext.SaveChangesAsync();

        return new DeleteDiscountResponse { Success = true };
    }

    public async override Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        var coupon = await dbContext
            .Coupons.FirstOrDefaultAsync(c => c.ProductName == request.ProductName);

        coupon ??= new Coupon { ProductName = "No Discount", Amount = 0, Description = "No Discount Desc" };

        logger.LogInformation("Discount is retrieved for ProductName : {porductName}, Amount : {amount}", coupon.ProductName, coupon.Amount);

        return coupon.Adapt<CouponModel>();
    }
}
