var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.StashManagement_API>("stashmanagement-api");

builder.AddNpmApp("stash-management", "../stash-management")
    .WithHttpEndpoint(targetPort: 53237);

builder.Build().Run();
