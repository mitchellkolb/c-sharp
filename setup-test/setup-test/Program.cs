// File: Program.cs
using System.Net.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Define a single GET endpoint at the root path
app.MapGet("/", async (HttpContext httpContext) =>
{
    var message = "Hello, world!";
    await httpContext.Response.WriteAsync(message);
});

app.Run();
