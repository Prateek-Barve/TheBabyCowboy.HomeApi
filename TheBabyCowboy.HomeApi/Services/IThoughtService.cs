using TheBabyCowboy.HomeApi.Models;

namespace TheBabyCowboy.HomeApi.Services;

public interface IThoughtService
{
    Task<Thought?> GetRandomThoughtAsync(
        CancellationToken cancellationToken = default);
}