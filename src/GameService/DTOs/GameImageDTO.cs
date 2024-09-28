namespace GameService.DTOs;

public class GameImageDTO
{
    public IFormFile file { get; set; }
    public Guid GameId { get; set; }   
}