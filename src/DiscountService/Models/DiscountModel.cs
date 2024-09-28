namespace DiscountService.Models;

public class DiscountModel
{
    public string CouponCode { get; set; }
    public string GameId { get; set; } 
    public int DiscountAmount { get; set; }
    public string? UserId { get; set; } 
}