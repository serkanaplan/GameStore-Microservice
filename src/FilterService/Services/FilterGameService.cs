using Elastic.Clients.Elasticsearch.QueryDsl;
using Elastic.Clients.Elasticsearch;
using FilterService.Models;

namespace FilterService.Services;


public class FilterGameService(ElasticsearchClient elasticSearch, IConfiguration configuration) : IFilterGameService
{
    private readonly ElasticsearchClient _elasticSearch = elasticSearch;
    public string indexName = configuration.GetValue<string>("indexName");


    public async Task<List<GameFilterItem>> SearchAsync(GameFilterItem gameFilterItem)
    {
        List<Action<QueryDescriptor<GameFilterItem>>> listQuery = [];
        if (gameFilterItem is null)
        {
            listQuery.Add(q=>q.MatchAll(new MatchAllQuery()));
            return await CalculateResultSet(listQuery);
        }
        
        if (!string.IsNullOrEmpty(gameFilterItem.Price.ToString()) && gameFilterItem.Price != 0)
            listQuery.Add((q) => q.Range(m=>m.NumberRange(f=>f.Field(a=>a.Price).Gte(Convert.ToDouble(gameFilterItem.Price)))));
          
        if (!string.IsNullOrEmpty(gameFilterItem.Price.ToString()) && gameFilterItem.Price != 0)
            listQuery.Add((q) => q.Range(m=>m.NumberRange(f=>f.Field(a=>a.Price).Lte(Convert.ToDouble(gameFilterItem.Price)))));
        
        if (!string.IsNullOrEmpty(gameFilterItem.MinimumSystemRequirement))
            listQuery.Add((q) => q.Wildcard(m=>m.Field(f=>f.MinimumSystemRequirement)
            .Value("*"+gameFilterItem.MinimumSystemRequirement+"*")));
       
        if (!string.IsNullOrEmpty(gameFilterItem.RecommendedSystemRequirement))
            listQuery.Add((q) => q.Wildcard(m=>m.Field(f=>f.RecommendedSystemRequirement)
            .Value("*"+gameFilterItem.RecommendedSystemRequirement+"*")));
      
        if (listQuery.Count == 0) listQuery.Add(q=>q.MatchAll(new MatchAllQuery()));

        return await CalculateResultSet(listQuery);
    }

    private async Task<List<GameFilterItem>> CalculateResultSet(List<Action<QueryDescriptor<GameFilterItem>>> listQuery)
    {
        var result = await _elasticSearch.SearchAsync<GameFilterItem>(x=>x.Index(indexName).Query(a=>a.Bool(b=>b.Must(listQuery.ToArray()))));
        return [.. result.Documents];
    }
}