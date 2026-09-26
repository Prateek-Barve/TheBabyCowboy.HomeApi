namespace TheBabyCowboy.HomeApi.Services;

public interface IAIResponseService
{
    Task<string> GenerateResponseAsync(
        string thought,
        string visitorResponse,
        CancellationToken cancellationToken = default);
}