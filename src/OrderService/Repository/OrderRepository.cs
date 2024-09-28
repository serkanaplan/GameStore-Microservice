using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using OrderService.Data;
using OrderService.Entities;

namespace OrderService.Repository;

public class OrderRepository(ApplicationDbContext context, IHttpContextAccessor contextAccessor) : IOrderRepository
{
    private readonly ApplicationDbContext _context = context;
    private readonly string UserId = contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;


    public async Task<List<Order>> GetOrderByUserId()
    {
        var result = await _context.Orders.Where(x=>x.UserId == Guid.Parse(UserId) && !x.IsPaid).ToListAsync();
        if (result is not null) return result;
        
        return null;
    }
}