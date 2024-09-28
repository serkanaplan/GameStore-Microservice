namespace GameService.Entities.Base;

public abstract class BaseModel
{
    public BaseModel()
    {
        Id = Guid.NewGuid();
        CreatedDate = DateTime.UtcNow;
    }

    public Guid Id { get; set; }
    public DateTime CreatedDate { get; set; }

}