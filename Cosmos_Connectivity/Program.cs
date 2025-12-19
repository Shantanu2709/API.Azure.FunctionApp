using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Cosmos_Connectivity.AD.CosmosDB;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication() // REQUIRED
    .ConfigureAppConfiguration((hostContext, config) =>
    {
        config.AddJsonFile("local.setting.json", optional: true, reloadOnChange: true)
              .AddEnvironmentVariables();
    })
    .ConfigureServices((hostContext, services) =>
    {
        var config = hostContext.Configuration;

        string connString = config["CosmosConnectionString"];   // FIXED key
        string dbName = config["CosmosDBName"];           // FIXED key
        string containerName = config["CosmosContainerName"];     // FIXED key

        // Log loaded values to confirm not null
        Console.WriteLine($"Cosmos Conn: {connString}");
        Console.WriteLine($"DB Name: {dbName}");
        Console.WriteLine($"Container: {containerName}");

        services.AddSingleton<ICosmosDB>(sp =>
            new CosmosService(connString, dbName, containerName));
    })
    .Build();

host.Run();
