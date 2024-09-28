using GameService.Repositories.ForGameImage;
using Microsoft.AspNetCore.Mvc;
using GameService.DTOs;

namespace GameService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GameImageController(IGameImageRepository gameImage) : ControllerBase
{
    private readonly IGameImageRepository _gameImage = gameImage;


    [HttpPost]
    public async Task<ActionResult> CreateImage([FromForm] GameImageDTO model) => Ok(await _gameImage.CreateGameImage(model));
}