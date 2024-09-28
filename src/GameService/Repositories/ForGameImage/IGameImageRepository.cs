using GameService.DTOs;

namespace GameService.Repositories.ForGameImage;

public interface IGameImageRepository
{
       Task<bool> CreateGameImage(GameImageDTO imageDTO);
}