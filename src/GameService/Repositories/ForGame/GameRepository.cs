using System.Security.Claims;
using AutoMapper;
using Contracts;
using GameService.Data;
using GameService.DTOs;
using GameService.Entities;
using GameService.Entities.Base;
using GameService.Services;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace GameService.Repositories.ForGame;

public class GameRepository(GameDbContext context, IHttpContextAccessor httpContextAccessor, IMapper mapper, IFileService fileService, BaseResponseModel responseModel, IPublishEndpoint publishEndpoint) : IGameRepository
{
    private readonly GameDbContext _context = context;
    private readonly IMapper _mapper = mapper;
    private readonly IFileService _fileService = fileService;
    private BaseResponseModel _responseModel = responseModel;
    private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;
    private readonly string UserId =  httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

    public async Task<BaseResponseModel> CreateGame(GameDTO game)
    {
        if (game.VideoFile.Length > 0 && game.GameFile.Length > 0)
        {
            string videoUrl = await _fileService.UploadVideo(game.VideoFile);
            string gameUrl = await _fileService.UploadZip(game.GameFile);
            var objDTO = _mapper.Map<Game>(game);
            objDTO.VideoUrl = videoUrl;
            objDTO.GameInfo = gameUrl;
            objDTO.UserId = UserId;
            await _context.Games.AddAsync(objDTO);
            await _publishEndpoint.Publish(_mapper.Map<GameCreated>(objDTO));

            if (await _context.SaveChangesAsync() > 0)
            {
                _responseModel.IsSuccess = true;
                _responseModel.Message = "Created Game Successfully";
                _responseModel.Data = objDTO;
                return _responseModel;
            }
        }
        _responseModel.IsSuccess = false;
        return _responseModel;
    }


    public async Task<BaseResponseModel> GetAllGames()
    {
        List<Game> games = await _context.Games.Include(x => x.Category).Include(x => x.GameImages).ToListAsync();
        if (games is not null)
        {
            _responseModel.Data = games;
            _responseModel.IsSuccess = true;
            return _responseModel;
        }
        _responseModel.IsSuccess = false;
        return _responseModel;
    }


    public async Task<BaseResponseModel> GetGamesByCategory(Guid categoryId)
    {
        List<Game> games = await _context.Games.Include(x => x.GameImages).Where(x => x.CategoryId == categoryId).ToListAsync();
        if (games is not null)
        {
            _responseModel.Data = games;
            _responseModel.IsSuccess = true;
            return _responseModel;
        }
        _responseModel.IsSuccess = false;
        return _responseModel;
    }


    public async Task<BaseResponseModel> RemoveGame(Guid gameId)
    {
        Game game = await _context.Games.FindAsync(gameId);
        if (game is not null)
        {
            _context.Games.Remove(game);
            await _publishEndpoint.Publish<GameDeleted>(new { Id = gameId.ToString() });
            if (await _context.SaveChangesAsync() > 0)
            {
                _responseModel.IsSuccess = true;
                _responseModel.Data = game;
                return _responseModel;
            }
        }
        _responseModel.IsSuccess = false;
        return _responseModel;
    }


    public async Task<BaseResponseModel> UpdateGame(UpdateGameDTO game, Guid gameId)
    {
        Game gameObj = await _context.Games.FindAsync(gameId);
        if (gameObj is not null)
        {
            gameObj.Price = game.Price;
            gameObj.RecommendedSystemRequirement = game.RecommendedSystemRequirement;
            gameObj.MinimumSystemRequirement = game.MinimumSystemRequirement;
            gameObj.GameAuthor = game.GameAuthor;
            gameObj.GameName = game.GameName;
            gameObj.GameDescription = game.GameDescription;
            await _publishEndpoint.Publish(_mapper.Map<GameUpdated>(gameObj));
            if (await _context.SaveChangesAsync() > 0)
            {
                _responseModel.IsSuccess = true;
                _responseModel.Data = gameObj;
                return _responseModel;
            }
        }
        _responseModel.IsSuccess = false;
        return _responseModel;
    }


    public async Task<BaseResponseModel> GetGameById(Guid gameId)
    {
        var result = await _context.Games.Include(x => x.GameImages).FirstOrDefaultAsync(x => x.Id == gameId);
        if (result is not null)
        {
            _responseModel.Data = result;
            _responseModel.IsSuccess = true;
            return _responseModel;
        }
        _responseModel.IsSuccess = false;
        return _responseModel;
    }


    public async Task<BaseResponseModel> GetMyGames()
    {
        List<MyGame> result = await _context.MyGames.Where(x => x.UserId == Guid.Parse(UserId)).ToListAsync();
        List<Game> games = [];
        foreach (var item in result)
        {
            var response = await _context.Games.FirstOrDefaultAsync(x => x.Id == item.GameId);
            if (response is not null) games.Add(response);
        }
        _responseModel.IsSuccess = true;
        _responseModel.Data = games;
        return _responseModel;
    }
}