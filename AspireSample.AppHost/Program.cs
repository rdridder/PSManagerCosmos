var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache");


var serviceBus = builder.AddConnectionString("ASB");

//var cosmos = builder.AddAzureCosmosDB("cosmosdb").AddDatabase("ProcessApi");
//cosmos.WithHttpsEndpoint(8081, 8081, "emulator-port").RunAsEmulator();
//https://localhost:8081/


var apiService = builder.AddProject<Projects.AspireSample_ApiService>("apiservice");

builder.AddProject<Projects.AspireSample_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(cache)
//    .WithReference(cosmos)
    .WithReference(apiService);

builder.AddProject<Projects.AspireSample_ProcessApi>("aspiresample-processapi").WithReference(serviceBus);

builder.AddProject<Projects.ProcessFunctions>("processfunctions").WithReference(serviceBus);

//builder.AddProject<Projects.SampleFunctions>("samplefunctions");


builder.Build().Run();
