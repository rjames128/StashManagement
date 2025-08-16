var builder = DistributedApplication.CreateBuilder(args);

var db = builder.AddPostgres("DB")
    .AddDatabase("stash");

var aws = builder.AddContainer("AWS", "localstack/localstack", "stable").
    WithHttpsEndpoint(name: "aws", targetPort: 4566);

var bff = builder.AddProject<Projects.StashManagement_API>("stashmanagement-api")
    .WaitFor(aws)
    .WaitFor(db)
    .WithReference(db)
    .WithEnvironment("aws", aws.GetEndpoint("aws"));

builder.AddNpmApp("stash-management-ui", "../stash-management")
    .WithHttpEndpoint(targetPort: 53237)
    .WaitFor(bff)
    .WithReference(bff);

builder.Build().Run();
