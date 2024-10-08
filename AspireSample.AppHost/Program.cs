var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache");

//var cosmos = builder.AddAzureCosmosDB("cosmosdb").AddDatabase("ProcessApi");
//cosmos.WithHttpsEndpoint(8081, 8081, "emulator-port").RunAsEmulator();
//https://localhost:8081/


var apiService = builder.AddProject<Projects.AspireSample_ApiService>("apiservice");

builder.AddProject<Projects.AspireSample_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(cache)
//    .WithReference(cosmos)
    .WithReference(apiService);

builder.AddProject<Projects.AspireSample_ProcessApi>("aspiresample-processapi");

builder.AddProject<Projects.ProcessFunctions>("processfunctions");

builder.Build().Run();
