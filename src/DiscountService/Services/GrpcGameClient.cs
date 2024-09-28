using DiscountService.Models;
using GameService;
using Grpc.Net.Client;

namespace DiscountService.Services;

public class GrpcGameClient(ILogger<GrpcGameClient> logger, IConfiguration configuration)
{
    private readonly ILogger<GrpcGameClient> _logger = logger;
    private readonly IConfiguration _configuration = configuration;

    public Game GetGame(string gameId,string userId)
    {
        _logger.LogWarning("Calling grpc protobuf service");
        var channel = GrpcChannel.ForAddress(_configuration["GrpcGame"]);
        var client = new GrpcGame.GrpcGameClient(channel);
        var request = new GetGameRequest{ Id = gameId,UserId = userId };
        try
        {
            var response = client.GetGame(request);
            Game game = new()
            {
                GameName = response.Game.GameName,
                Price = Convert.ToDecimal(response.Game.Price),
                VideoUrl = response.Game.VideoUrl,
                GameDescription = response.Game.GameDescription,
                MinimumSystemRequirement = response.Game.MinimumSystemRequirement,
                RecommendedSystemRequirement = response.Game.RecommendedSystemRequirement,
                UserId = response.Game.UserId,
                CategoryId = Guid.Parse(response.Game.CategoryId),  
            };
            Console.WriteLine(response);
            return game;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw ex;
        }
    }
}