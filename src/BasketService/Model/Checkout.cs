namespace BasketService.Model;

public class Checkout
{
  public Guid GameId { get; set; }
  public string GameName { get; set; }
  public string GameAuthor { get; set; }
  public decimal Price { get; set; }
  public string GameDescription { get; set; }
  public Guid UserId { get; set; }
}