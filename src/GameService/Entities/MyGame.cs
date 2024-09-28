using GameService.Entities.Base;

namespace GameService.Entities;

public class MyGame : BaseModel
{
    public Guid UserId { get; set; }
    public Guid GameId { get; set; }
}