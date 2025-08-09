var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.StashManagement_API>("stashmanagement-api");

builder.Build().Run();
