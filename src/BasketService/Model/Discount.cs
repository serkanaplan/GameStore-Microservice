namespace BasketService.Model;


public class Discount
{
    public int Id { get; set; }
    public DateTime ExpireDate { get; set; }
    public string CouponCode { get; set; }
    public int DiscountAmount { get; set; }
    public string GameId { get; set; } 
    public string UserId { get; set; } 
}