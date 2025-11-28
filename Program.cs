using Microsoft.EntityFrameworkCore;
using TodoApi.Data;
using TodoApi.Services;
using TodoApi.Services.ServiceInterface;
using TodoApi.Repositories;
using TodoApi.Repositories.ITodoRepository;

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
builder.Services.AddScoped<ITodoRepository, TodoRepositories>();
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi(Config =>
    {

    })
}