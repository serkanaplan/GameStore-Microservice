using System.Security.Claims;
using DiscountService.Data;
using DiscountService.Entities;
using DiscountService.Models;
using DiscountService.Services;

namespace DiscountService.Repository;

public class DiscountRepository(AppDbContext context, GrpcGameClient grpcClient, IHttpContextAccessor contextAccessor) : IDiscountRepository
{
    private readonly AppDbContext _context = context;
    private readonly GrpcGameClient _grpcClient = grpcClient;
    private readonly string UserId = contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

    public async Task<bool> CreateDiscount(DiscountModel model)
    {
        try
        {
            if (model != null)
            {
                Console.WriteLine("trigger inner discount ----> " + model.UserId);
                var game = _grpcClient.GetGame(model.GameId, UserId);
                if (!string.IsNullOrEmpty(game.GameName))
                {
                    Discount discount = new()
                    {
                        CouponCode = model.CouponCode,
                        DiscountAmount = model.DiscountAmount,
                        GameId = model.GameId,
                        UserId = game.UserId,
                    };
                    await _context.Discounts.AddAsync(discount);
                    if (await _context.SaveChangesAsync() > 0) return true;
                }
            }
            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw ex;
        }
    }
}