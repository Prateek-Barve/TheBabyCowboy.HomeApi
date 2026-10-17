using Microsoft.Azure.Cosmos;
using TheBabyCowboy.HomeApi.Configuration;

namespace TheBabyCowboy.HomeApi.Data;

public class CosmosDbContext
{
    public Container Thoughts { get; }

    public CosmosDbContext(
        CosmosClient cosmosClient,
        CosmosDbSettings settings)
    {
        Thoughts = cosmosClient.GetContainer(
            settings.DatabaseName,
            settings.ContainerName);
    }
}