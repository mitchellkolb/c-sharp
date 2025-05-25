using Microsoft.EntityFrameworkCore;
using TallyBoard.Data;

var builder = WebApplication.CreateBuilder(args);

// configure in-memory EF Core
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseInMemoryDatabase("TallyDb"));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();
