namespace KingPrice.Core.Services;

using Entities;

public class SeedService
{
    public Task SeedDatabase(AppDbContext context)
    {

        if (context.Permissions.Any() || context.Groups.Any()) 
        {
            return Task.CompletedTask;
        }

        // Create permissions (Level 1, 2, 3)
        var permissions = CreatePermissions();
        context.Permissions.AddRange(permissions);
        context.SaveChanges();

        var groups = CreateGroups(permissions);
        context.Groups.AddRange(groups);
        context.SaveChanges();

        return Task.CompletedTask;
    }

    private static List<Permission> CreatePermissions()
    {
        return
        [
            new Permission
            {
                Name = "Level1",
            },

            new Permission
            {
                Name = "Level2",
            },

            new Permission
            {
                Name = "Level3",
            },
        ];
    }


    private static List<Group> CreateGroups(List<Permission> allPermissions)
    {
        var groups = new List<Group>();

        // Get individual permission levels
        var level1Permission = allPermissions.FirstOrDefault(p => p.Name == "Level1") ?? new Permission { Name = "Level1" };
        var level2Permission = allPermissions.FirstOrDefault(p => p.Name == "Level2") ?? new Permission { Name = "Level2" };
        var level3Permission = allPermissions.FirstOrDefault(p => p.Name == "Level3") ?? new Permission { Name = "Level3" };

        // Viewers - Level 1 Access (Read Only)
        var users = new Group
        {
            Name = "Viewers",
            Permissions = [level1Permission],
        };
        groups.Add(users);

        // Managers - Level 2 Access (Read & Write)
        var managers = new Group
        {
            Name = "Managers",
            Permissions = [level1Permission, level2Permission],
        };
        groups.Add(managers);

        // Administrators - Level 3 Access (Full Admin)
        var administrators = new Group
        {
            Name = "Administrators",
            Permissions = [level1Permission, level2Permission, level3Permission],
        };
        groups.Add(administrators);

        return groups;
    }
    
}
