namespace DiscountService.Models;

public class Game
{
    public string GameName { get; set; }
    public string GameAuthor { get; set; }
    public decimal Price { get; set; }
    public string VideoUrl { get; set; }
    public string GameDescription { get; set; }
    public string MinimumSystemRequirement { get; set; }
    public string RecommendedSystemRequirement { get; set; }
    public string UserId { get; set; }
    public Guid CategoryId { get; set; }
}