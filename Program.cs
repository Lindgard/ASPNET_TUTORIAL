using Microsoft.EntityFrameworkCore;
using TodoApi.Data;
using TodoApi.Services;
using TodoApi.Controllers;
using ITodoRepository = ITodoRepository.ITodoRepository;
using ITodoService = ITodoService.ITodoService;
using Repositories;

var builder = WebApplication.CreateBuilder(args);

//* Database
builder.Services.AddDbContext<TodoDb>(opt => opt.UseInMemoryDatabase("TodoList"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

//* OpenAPI/Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiDocument(config =>
{
    config.DocumentName = "TodoAPI";
    config.Title = "TodoAPI v2";
    config.Version = "v2";
});

//* Dependency injection
builder.Services.AddScoped<ITodoRepository, TodoRepository>();
builder.Services.AddScoped<ITodoService, TodoService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi(config =>
    {
        config.Path = "/swagger";
    });
}

app.MapTodoController();
app.Run();