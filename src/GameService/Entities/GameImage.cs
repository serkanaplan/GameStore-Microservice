using System.Text.Json.Serialization;
using GameService.Entities.Base;

namespace GameService.Entities;

public class GameImage : BaseModel
{
    public string ImageUrl { get; set; }
    public Guid GameId { get; set; }   

    [JsonIgnore ]
    public Game Game { get; set; }
}