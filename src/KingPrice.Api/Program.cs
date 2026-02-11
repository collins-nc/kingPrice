using KingPrice.Api;
using KingPrice.Api.Endpoints;
using KingPrice.Core;
using KingPrice.Core.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.AddSqlServerDbContext<AppDbContext>("kingPriceDb");
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<SeedService>();

var app = builder.Build();
app.InitializeDatabase();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// Map Endpoints
app.MapUserEndpoints();

app.Run();
