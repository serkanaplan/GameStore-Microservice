using Contracts;
using MassTransit;
using MongoDB.Entities;
using SearchService.Models;

namespace SearchService.Consumers;


public class GameDeletedConsumer : IConsumer<GameDeleted>
{
    public async Task Consume(ConsumeContext<GameDeleted> context)
    {
        Console.WriteLine("---> Game Deleted Consuming"+context.Message.Id);
        var result = await DB.DeleteAsync<GameItem>(context.Message.Id);
        if (!result.IsAcknowledged) throw new MessageException(typeof(GameDeleted),"Problem having");
    }
}