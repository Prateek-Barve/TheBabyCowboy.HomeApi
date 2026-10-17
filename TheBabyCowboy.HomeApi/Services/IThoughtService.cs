//using TheBabyCowboy.HomeApi.Models;

//namespace TheBabyCowboy.HomeApi.Services;

//public interface IThoughtService
//{
//    Task<Thought?> GetRandomThoughtAsync(
//        CancellationToken cancellationToken = default);
//}
using TheBabyCowboy.HomeApi.Models;

namespace TheBabyCowboy.HomeApi.Services;

public interface IThoughtService
{
    Task<Thought?> GetRandomThoughtAsync(
        CancellationToken cancellationToken = default);

    Task<Thought?> GetThoughtByIdAsync(
        string thoughtId,
        CancellationToken cancellationToken = default);
}