using AutoMapper;
using Contracts;
using Elastic.Clients.Elasticsearch;
using FilterService.Models;
using MassTransit;

namespace FilterService.Consumer;


public class GameCreatedFilterConsumer(IMapper mapper, ElasticsearchClient elasticClient, IConfiguration configuration) : IConsumer<GameCreated>
{
    private readonly IMapper _mapper = mapper;
    private readonly ElasticsearchClient _elasticClient = elasticClient;
    
    public async Task Consume(ConsumeContext<GameCreated> context)
    {
        Console.WriteLine("Consuming Filter Service For Created Game ----> " + context.Message.GameName);
        var objDTO = _mapper.Map<GameFilterItem>(context.Message);
        objDTO.GameId = context.Message.Id.ToString();

        var elasticSearch = await _elasticClient.IndexAsync(objDTO, x => x.Index(configuration.GetValue<string>("indexName")));
        if (!elasticSearch.IsValidResponse)
        {
            Console.WriteLine(elasticSearch);
            Console.WriteLine("Consuming process is not valid");
        }
    }
}