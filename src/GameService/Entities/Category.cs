using GameService.Entities.Base;
namespace GameService.Entities;

public class Category : BaseModel
{
    public string CategoryName { get; set; }
    public string CategoryDescription { get; set; }
    public ICollection<Game> Game { get; set; }
}