using Elastic.Clients.Elasticsearch;
using Elastic.Transport;

namespace FilterService.Extensions;


public static class ElasticExtension
{
    public static void AddElastic(this IServiceCollection services,IConfiguration configuration)
    {
        var userName = configuration.GetSection("Elastic")["Username"];
        var password = configuration.GetSection("Elastic")["Password"];
        var settings = new ElasticsearchClientSettings(new Uri(configuration.GetSection("Elastic")["Url"]))
            // .Authentication(new BasicAuthentication(userName,password));
            .Authentication(new BasicAuthentication("elastic"!,"Passw0rd"!));
        
        var client = new ElasticsearchClient(settings);
        services.AddSingleton(client);
    }
}