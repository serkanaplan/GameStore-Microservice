using System.Text.Json.Serialization;

namespace FilterService.Models;

public class GameFilterItem
{
    [JsonPropertyName("gameId")]
    public string GameId { get; set; }

    [JsonPropertyName("gameName")]
    public string GameName { get; set; }

    [JsonPropertyName("gameAuthor")]
    public string GameAuthor { get; set; }

    [JsonPropertyName("price")]
    public decimal Price { get; set; }  

    [JsonPropertyName("gameDescription")]
    public string GameDescription { get; set; } 

    [JsonPropertyName("minimumSystemRequirement")]
    public string MinimumSystemRequirement { get; set; }    

    [JsonPropertyName("recommendedSystemRequirement")]
    public string RecommendedSystemRequirement { get; set; }    

    [JsonPropertyName("categoryId")]
    public Guid CategoryId { get; set; }
}