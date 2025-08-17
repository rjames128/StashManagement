using StashManagement.API.Startup;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
var configuration = builder.Configuration;
builder.Services.ConfigureServices(configuration);

// Add services to the container.

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapRoutes();

app.Run();