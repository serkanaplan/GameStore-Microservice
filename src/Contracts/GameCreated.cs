namespace Contracts;

public class GameCreated
{
    public Guid Id { get; set; }
     public string GameName { get; set; }
    public string GameAuthor { get; set; }
    public decimal Price { get; set; }  
    public string GameDescription { get; set; } 
    public string MinimumSystemRequirement { get; set; }    
    public string RecommendedSystemRequirement { get; set; }    
    public Guid CategoryId { get; set; }
}