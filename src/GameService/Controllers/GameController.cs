using GameService.DTOs;
using GameService.Repositories.ForGame;
using GameService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameService.Controllers;

[ApiController]
[Route("[controller]")]
public class GameController(IGameRepository gameRepository, IFileService fileService) : ControllerBase
{
    private readonly IGameRepository _gameRepository = gameRepository;
    private readonly IFileService _fileService = fileService;


    [HttpPost]
    [Authorize]
    public async Task<ActionResult> CreateGame([FromForm]GameDTO game)
    {
        Console.WriteLine(game);
        Console.WriteLine(game.GameName);
        Console.WriteLine(game.GameDescription);
        Console.WriteLine(game.GameAuthor);
        Console.WriteLine(game.Price);
        Console.WriteLine(game.GameFile);
        Console.WriteLine(game.VideoFile);
        if (ModelState.IsValid)
        {
            if (game.GameFile == null || game.VideoFile == null) return BadRequest("inner");
            return Ok(await _gameRepository.CreateGame(game));   
        }
        return BadRequest("outer");
    }


    [HttpDelete("{gameId}")]
    public async Task<ActionResult> RemoveGame([FromRoute]Guid gameId)
    {
        return Ok(await _gameRepository.RemoveGame(gameId));
    }


    [HttpGet]
    public async Task<ActionResult> GetAllGames() => Ok(await _gameRepository.GetAllGames());


    [HttpPost("Download")]
    public async Task<ActionResult> DownloadGame(string fileUrl) => Ok(await _fileService.DownloadGame(fileUrl));


    [HttpGet("{categoryId}")]
    public async Task<ActionResult> GetGamesByCategoryId([FromRoute] Guid categoryId) 
    => Ok(await _gameRepository.GetGamesByCategory(categoryId));


    [HttpPut("{gameId}")]
    public async Task<ActionResult> UpdateGame(UpdateGameDTO model, [FromRoute] Guid gameId) 
    => Ok(await _gameRepository.UpdateGame(model, gameId));


    [HttpGet("game/{gameId}")]
    public async Task<ActionResult> GetGameById([FromRoute] Guid gameId) => Ok(await _gameRepository.GetGameById(gameId));


    [HttpGet("mygames")]
    [Authorize]
    public async Task<ActionResult> GetMyGames() => Ok(await _gameRepository.GetMyGames());

}