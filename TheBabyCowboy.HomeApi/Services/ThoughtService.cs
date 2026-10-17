//using MongoDB.Driver;
//using TheBabyCowboy.HomeApi.Data;
//using TheBabyCowboy.HomeApi.Models;

//namespace TheBabyCowboy.HomeApi.Services;

//public class ThoughtService : IThoughtService
//{
//    private readonly MongoDbContext _mongoDbContext;

//    public ThoughtService(MongoDbContext mongoDbContext)
//    {
//        _mongoDbContext = mongoDbContext;
//    }

//    public async Task<Thought?> GetRandomThoughtAsync(
//        CancellationToken cancellationToken = default)
//    {
//        var thoughts = await _mongoDbContext.Thoughts
//            .Find(FilterDefinition<Thought>.Empty)
//            .ToListAsync(cancellationToken);

//        if (thoughts.Count == 0)
//        {
//            return null;
//        }

//        var randomIndex = Random.Shared.Next(thoughts.Count);

//        return thoughts[randomIndex];
//    }
//}
using Microsoft.Azure.Cosmos;
using TheBabyCowboy.HomeApi.Data;
using TheBabyCowboy.HomeApi.Models;

namespace TheBabyCowboy.HomeApi.Services;

public class ThoughtService : IThoughtService
{
    private readonly Container _thoughtsContainer;

    public ThoughtService(CosmosDbContext cosmosDbContext)
    {
        _thoughtsContainer = cosmosDbContext.Thoughts;
    }

    public async Task<Thought?> GetThoughtByIdAsync(
        string thoughtId,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(thoughtId))
            return null;

        try
        {
            var response = await _thoughtsContainer.ReadItemAsync<Thought>(
                id: thoughtId,
                partitionKey: new PartitionKey(thoughtId),
                cancellationToken: cancellationToken);

            return response.Resource;
        }
        catch (CosmosException ex) when (
            ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null;
        }
    }

    public async Task<Thought?> GetRandomThoughtAsync(
        CancellationToken cancellationToken = default)
    {
        var query = new QueryDefinition(
            "SELECT * FROM c");

        using var iterator =
            _thoughtsContainer.GetItemQueryIterator<Thought>(query);

        var thoughts = new List<Thought>();

        while (iterator.HasMoreResults)
        {
            var response =
                await iterator.ReadNextAsync(cancellationToken);

            thoughts.AddRange(response);
        }

        if (thoughts.Count == 0)
            return null;

        var randomIndex = Random.Shared.Next(thoughts.Count);
        return thoughts[randomIndex];
    }
}