var bucketName = "stashmanagement-bucket";

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
    WithEnvironment("SERVICES", "s3").
    WithEnvironment("AWS_REGION", "us-east-1").
    WithEnvironment("AWS_ACCESS_KEY_ID", "key").
    WithEnvironment("AWS_SECRET_ACCESS_KEY", "secret").
    WithContainerFiles("/etc/localstack/init/",
        [new ContainerDirectory
        {
            Name = "ready.d",
            Entries = [
                new ContainerFile
                {
                    Name = "init-aws.sh",
                    Contents = $"""
                    #!/bin/sh

                    awslocal s3 mb s3://{bucketName}
                    """,
                    Mode = UnixFileMode.UserExecute | UnixFileMode.UserRead | UnixFileMode.UserWrite |
                        UnixFileMode.GroupRead | UnixFileMode.GroupWrite | UnixFileMode.GroupExecute |
                        UnixFileMode.OtherRead | UnixFileMode.OtherWrite | UnixFileMode.OtherExecute
                }
            ]
        }], defaultOwner: 1000).
    WithHttpsEndpoint(name: "aws", targetPort: 4566);

var bff = builder.AddProject<Projects.StashManagement_API>("stashmanagement-api")
    .WaitFor(aws)
    .WaitFor(db)
    .WithReference(db)
    .WithEnvironment("Aws__BucketName", bucketName)
    .WithEnvironment("Aws__IsLocal", "true")
    .WithEnvironment("Aws__Region", "us-east-1")
    .WithEnvironment("Aws__LocalUrl", aws.GetEndpoint("aws"))
    .WithEnvironment("Aws__AccessKey","key")
    .WithEnvironment("Aws__SecretKey", "secret")
    .WithEnvironment("Aws__LocalUrl", aws.GetEndpoint("aws"));

builder.AddNpmApp("stash-management-ui", "../stash-management")
    .WithHttpEndpoint(targetPort: 53237)
    .WaitFor(bff)
    .WithReference(bff);

builder.Build().Run();
