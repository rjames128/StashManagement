using Microsoft.AspNetCore.Mvc;
using StashManagement.API.Entities;

namespace StashManagement.API.Startup
{
    public static class RouteMapper
    {
        public static void MapRoutes(this WebApplication app)
        {
            app.MapGet("/fabrics", async ([FromServices]IStashItemRepository repository, [FromHeader] Guid profileId) =>
            {
                var items = await repository.GetAllAsync(profileId);
                return Results.Ok(items);
            })
            .Produces<IEnumerable<FabricItem>>(StatusCodes.Status200OK)
            .WithName("GetAllFabrics")
            .WithOpenApi();

            app.MapGet("/fabric/{id:guid}", async (Guid id, [FromServices]IStashItemRepository repository, [FromHeader()] Guid profileId) =>
            {
                var item = await repository.GetByIdAsync(id, profileId);
                return item is not null ? Results.Ok(item) : Results.NotFound();
            })
            .Produces<FabricItem>(StatusCodes.Status200OK)
            .WithName("GetFabricById")
            .WithOpenApi();

            app.MapPost("/fabric", async ([FromBody]FabricItem item, [FromServices]IStashItemRepository repository, [FromHeader()] Guid profileId) =>
            {
                await repository.AddAsync(profileId, item);
                return Results.Created($"/fabric/{item.Id}", item);
            })
            .Produces<FabricItem>(StatusCodes.Status201Created)
            .WithName("CreateFabric")
            .WithOpenApi();

            app.MapPut("/fabric/{id:guid}", async (Guid id, [FromBody]FabricItem item, [FromServices]IStashItemRepository repository, [FromHeader()] Guid profileId) =>
            {
                if (id != item.Id)
                {
                    return Results.BadRequest("ID mismatch");
                }
                var updated = await repository.UpdateAsync(profileId, item);
                return updated ? Results.NoContent() : Results.NotFound();
            })
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .WithName("UpdateFabricItem")
            .WithOpenApi();

            app.MapDelete("/fabric/{id:guid}", async (Guid id, [FromServices] IStashItemRepository repository, [FromHeader()] Guid profileId) =>
            {
                var deleted = await repository.DeleteAsync(id, profileId);
                return deleted ? Results.NoContent() : Results.NotFound();
            })
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .WithName("DeleteFabric")
            .WithOpenApi();
        }
    }
}
