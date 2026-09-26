using MongoDB.Driver;
using TheBabyCowboy.HomeApi.Data;
using TheBabyCowboy.HomeApi.DTOs;
using TheBabyCowboy.HomeApi.Models;

namespace TheBabyCowboy.HomeApi.Services;

public class ReflectionService : IReflectionService
{
    private readonly MongoDbContext _mongoDbContext;
    private readonly IAIResponseService _aiResponseService;

    public ReflectionService(
        MongoDbContext mongoDbContext,
        IAIResponseService aiResponseService)
    {
        _mongoDbContext = mongoDbContext;
        _aiResponseService = aiResponseService;
    }

    public async Task<ReflectionResponseDto> CreateReflectionAsync(
        CreateReflectionDto request,
        CancellationToken cancellationToken = default)
    {
        var thought = await _mongoDbContext.Thoughts
            .Find(x => x.Id == request.ThoughtId)
            .FirstOrDefaultAsync(cancellationToken);

        if (thought is null)
        {
            throw new KeyNotFoundException("Thought not found.");
        }

        // Temporary response.
        // We will replace this with the actual AI call next.
        //var aiResponse =
        //    "That's an interesting reflection. Sometimes the answer tells us more about ourselves than the question.";

        var aiResponse = await _aiResponseService.GenerateResponseAsync(
        thought.Text,
        request.VisitorResponse,
        cancellationToken);

        var reflection = new Reflection
        {
            ThoughtId = request.ThoughtId,
            VisitorResponse = request.VisitorResponse,
            AIResponse = aiResponse,
            CreatedAt = DateTime.UtcNow
        };

        await _mongoDbContext.Reflections
            .InsertOneAsync(
                reflection,
                cancellationToken: cancellationToken);

        return new ReflectionResponseDto
        {
            Id = reflection.Id,
            ThoughtId = reflection.ThoughtId,
            AIResponse = reflection.AIResponse
        };
    }
}