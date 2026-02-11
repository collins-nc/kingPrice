namespace KingPrice.Api;

using Core;
using Core.Entities;
using Core.Services;
using Microsoft.EntityFrameworkCore;

public static class WebApplicationExtensions
{
    public static void InitializeDatabase(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var config = scope.ServiceProvider.GetRequiredService<IConfiguration>();
        var seedService = scope.ServiceProvider.GetRequiredService<SeedService>();
        
        var seed = config.GetValue<bool>("Database:Seed");
        var drop = config.GetValue<bool>("Database:drop");
        var migrate = config.GetValue<bool>("Database:migrate");
        if (drop)
        {
            dbContext.Database.EnsureDeleted();
        }
        if (migrate)
        {
            dbContext.Database.Migrate();
        }
        if (seed)
        {
            seedService.SeedDatabase(dbContext);
        }
    }
    

}
