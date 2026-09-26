using TheBabyCowboy.HomeApi.DTOs;

namespace TheBabyCowboy.HomeApi.Services;

public interface IReflectionService
{
    Task<ReflectionResponseDto> CreateReflectionAsync(
        CreateReflectionDto request,
        CancellationToken cancellationToken = default);
}