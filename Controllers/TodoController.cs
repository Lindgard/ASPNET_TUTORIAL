using Microsoft.AspNetCore.Mvc;
using ServiceInterface;
using TodoApi.DTO;
using TodoApi.Services.ITodoService;

public static class TodoController
{
    public static void MapTodoController(this WebApplication app)
    {
        app.MapGet("/todoitems", async (ITodoService service) =>
            Results.Ok(await service.GetAllAsync()));

        app.MapGet("/todoitems/complete", async (ITodoService service) =>
            Results.Ok(await service.GetAllAsync()));

        app.MapGet("/todoitems/{id}", async (CreateTodoItemDto dto, ITodoService service) =>
        {
            var result = await service.GetByIdAsync(id);
            return result is null ? Results.NotFound() : Results.Ok(result);
        });

        app.MapPost("/todoitems", async (CreateTodoItemDto dto, ITodoService service) =>
        {
            var created = await service.CreateAsync(dto);
            return Results.Created($"/todoitems/{created.id}", created);
        });

        app.MapPut("/todoitems/{id}", async (int id, UpdateTodoItemDto dto, ITodoService service) =>
        {
            var updated = await service.UpdateAsync(id, dto);
            return updated ? Results.NoContent() : Results.NotFound();
        });

        app.MapDelete("/todoitems/{id}", async (int id, ITodoService service) =>
        {
            var deleted = await service.DeleteAsync(id);
            return deleted ? Results.NoContent() : Results.NotFound();
        });
    }
}