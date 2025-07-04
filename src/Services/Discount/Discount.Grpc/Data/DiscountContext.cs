using Discount.Grpc.Models;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Data
{
    public class DiscountContext : DbContext
    {
        public required DbSet<Coupon> Coupons{ get; set; }

        public DiscountContext(DbContextOptions<DiscountContext> options) : base(options)
        { 
        }
    }
}
