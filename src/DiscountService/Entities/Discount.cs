using System.ComponentModel.DataAnnotations;

namespace DiscountService.Entities;

public class Discount
{
    [Key]
    public int Id { get; set; }
    public DateTime ExpireDate { get; set; } = DateTime.UtcNow.AddDays(7);
    public string CouponCode { get; set; }
    public int DiscountAmount { get; set; }
    public string GameId { get; set; } 
    public string UserId { get; set; } 
}