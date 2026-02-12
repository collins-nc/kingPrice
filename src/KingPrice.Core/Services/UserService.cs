namespace KingPrice.Core.Services ;

using Abstraction.Dtos;
using Entities;
using Microsoft.EntityFrameworkCore;


public class UserService(AppDbContext context) : IUserService
{
    public async Task<UserResponse> CreateUserAsync(CreateUserRequest request)
    {
        var groups = new List<Group?>();

        // Handle groups - create if doesn't exist, otherwise fetch existing
        if (request.GroupNames.Count > 0)
        {
            foreach (var groupName in request.GroupNames)
            {
                // Check if group already exists
                var existingGroup = await context.Groups
                    .FirstOrDefaultAsync(g => g.Name == groupName);

                if (existingGroup != null)
                {
                    // Group exists, use it
                    groups.Add(existingGroup);
                }
                else
                {
                    // Group doesn't exist, create new one
                    var newGroup = new Group
                    {
                        Name = groupName,
                    };
                    groups.Add(newGroup);
                    context.Groups.Add(newGroup);
                }
            }

            // Save groups before creating user
            await context.SaveChangesAsync();
        }

        // Create user with groups
        var user = new User
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Groups = groups,
        };

        context.Users.Add(user);
        await context.SaveChangesAsync();

        return MapToResponse(user);
    }

    public async Task<UserResponse?> GetUserByIdAsync(int id)
    {
        var user = await context.Users
            .Include(u => u.Groups)
            .FirstOrDefaultAsync(u => u.Id == id);

        return user == null ? null : MapToResponse(user);
    }

    public async Task<List<UserResponse>> GetAllUsersAsync()
    {
        var users = await context.Users
            .Include(u => u.Groups)
            .ToListAsync();

        return users.Select(MapToResponse).ToList();
    }

    public async Task<UserResponse?> UpdateUserAsync(int id, UpdateUserRequest request)
    {
        var user = await context.Users
            .Include(u => u.Groups)
            .FirstOrDefaultAsync(u => u.Id == id);

        if (user == null)
            return null;

        // Update user properties
        user.FirstName = request.FirstName;
        user.LastName = request.LastName;

        // Handle group updates
        if (request.GroupNames.Count > 0)
        {
            // Clear existing groups
            user.Groups.Clear();
                // Fetch or create groups
                foreach (var groupName in request.GroupNames)
                {
                    var existingGroup = await context.Groups
                        .FirstOrDefaultAsync(g => g.Name == groupName);

                    if (existingGroup != null)
                    {
                        // Group exists, add to user
                        user.Groups.Add(existingGroup);
                    }
                    else
                    {
                        // Group doesn't exist, create new one
                        var newGroup = new Group
                        {
                            Name = groupName,
                        };
                        context.Groups.Add(newGroup);
                        user.Groups.Add(newGroup);
                    }
                }

                // Save new groups first
                await context.SaveChangesAsync();
        }
        else
        {
            // If no groups provided, clear all groups
            user.Groups.Clear();
        }

        context.Users.Update(user);
        await context.SaveChangesAsync();

        return MapToResponse(user);
        }

    public async Task<bool> DeleteUserAsync(int id)
    {
        var user = await context.Users
                .Include(u => u.Groups)
                .FirstOrDefaultAsync(u => u.Id == id);

        if (user == null)
        {
            return false;
        }

        context.Users.Remove(user);
        await context.SaveChangesAsync();

        return true;
        }

    public async Task<int> GetTotalUserCountAsync()
    {
        return await context.Users.CountAsync();
    }

    public async Task<List<UsersPerGroupResponse>> GetUsersPerGroupAsync()
    {
        var result = await context.Groups
            .Include(g => g.Users)
            .Select(g => new UsersPerGroupResponse
            {
                GroupId = g.Id,
                GroupName = g.Name,
                UserCount = g.Users.Count(u => u != null),
            })
            .ToListAsync();

        return result;
    }

    private static UserResponse MapToResponse(User user)
    {
        return new UserResponse
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            GroupIds = user.Groups.Where(g => g != null).Select(g => g!.Id).ToList(),
        };
    }
}
