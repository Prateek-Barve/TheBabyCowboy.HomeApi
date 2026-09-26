using MongoDB.Driver;
using TheBabyCowboy.HomeApi.Data;
using TheBabyCowboy.HomeApi.Models;

namespace TheBabyCowboy.HomeApi.Services;

public class ThoughtService : IThoughtService
{
    private readonly MongoDbContext _mongoDbContext;

    public ThoughtService(MongoDbContext mongoDbContext)
    {
        _mongoDbContext = mongoDbContext;
    }

    public async Task<Thought?> GetRandomThoughtAsync(
        CancellationToken cancellationToken = default)
    {
        var thoughts = await _mongoDbContext.Thoughts
            .Find(FilterDefinition<Thought>.Empty)
            .ToListAsync(cancellationToken);

        if (thoughts.Count == 0)
        {
            return null;
        }

        var randomIndex = Random.Shared.Next(thoughts.Count);

        return thoughts[randomIndex];
    }
}