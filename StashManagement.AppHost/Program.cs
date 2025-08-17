var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("DB")
    .WithPgAdmin();

var db = postgres.AddDatabase("stash", "postgres")
    .WithCreationScript("""
        CREATE TABLE stash_item (
        id UUID PRIMARY KEY,
        profile_id UUID NOT NULL,
        name TEXT NOT NULL,
        description TEXT,
        source_location TEXT,
        created_at TIMESTAMP NOT NULL,
        updated_at TIMESTAMP NOT NULL,
        image_src TEXT
    );

        CREATE TABLE fabric_item (
        id UUID PRIMARY KEY REFERENCES stash_item(id),
        cut TEXT,
        amount NUMERIC
    );
    """);

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
