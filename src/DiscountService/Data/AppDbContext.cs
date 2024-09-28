using DiscountService.Entities;
using Microsoft.EntityFrameworkCore;

namespace DiscountService.Data;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Discount> Discounts { get; set; }
}