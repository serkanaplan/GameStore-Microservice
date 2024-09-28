using GameService.Data;
using GameService.DTOs;
using GameService.Entities;
using GameService.Services;

namespace GameService.Repositories.ForGameImage;

public class GameImageRepository(IFileService fileService, GameDbContext context) : IGameImageRepository
{
    private readonly IFileService _fileService = fileService;

    private readonly GameDbContext _context = context;


    public async Task<bool> CreateGameImage(GameImageDTO imageDTO)
    {
        string filePath = await _fileService.UploadImage(imageDTO.file);

        GameImage game = new(){GameId = imageDTO.GameId,ImageUrl = filePath };

        await _context.GameImages.AddAsync(game);
        if (await _context.SaveChangesAsync() > 0) return true;
        return false;
    }
}