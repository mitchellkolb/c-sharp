using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Linq;

var builder = WebApplication.CreateBuilder(args);

// (optional) configure JSON options, e.g. camelCase everywhere
builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.PropertyNamingPolicy = 
        System.Text.Json.JsonNamingPolicy.CamelCase;
});

var app = builder.Build();

// In-memory store
var todoItems = new List<TodoItem>();
var nextId = 1;

// GET /todos
app.MapGet("/todos", () => Results.Ok(todoItems));

// GET /todos/{id}
app.MapGet("/todos/{id:int}", (int id) =>
{
    var todoItem = todoItems.FirstOrDefault(t => t.Id == id);
    return todoItem is not null 
        ? Results.Ok(todoItem) 
        : Results.NotFound();
});

// POST /todos
app.MapPost("/todos", (TodoItem newItem) =>
{
    newItem.Id = nextId++;
    todoItems.Add(newItem);
    return Results.Created($"/todos/{newItem.Id}", newItem);
});

// PUT /todos/{id}
app.MapPut("/todos/{id:int}", (int id, TodoItem updatedItem) =>
{
    var existingItem = todoItems.FirstOrDefault(t => t.Id == id);
    if (existingItem is null) return Results.NotFound();

    // update fields
    existingItem.Name = updatedItem.Name;
    existingItem.IsComplete = updatedItem.IsComplete;
    return Results.NoContent();
});

// DELETE /todos/{id}
app.MapDelete("/todos/{id:int}", (int id) =>
{
    var todoItem = todoItems.FirstOrDefault(t => t.Id == id);
    if (todoItem is null) return Results.NotFound();

    todoItems.Remove(todoItem);
    return Results.NoContent();
});

app.Run();

// Model
public class TodoItem
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public bool IsComplete { get; set; }
}
