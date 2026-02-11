namespace KingPrice.Api.Endpoints
{
    using KingPrice.Core;
    using KingPrice.Core.DTOs;
    using Microsoft.AspNetCore.Http.HttpResults;

    public static class UserEndpoints
    {
        public static void MapUserEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/api/users")
                .WithName("Users")
                .WithOpenApi();

            group.MapPost("/", CreateUser)
                .WithName("CreateUser")
                .WithSummary("Create a new user");

            group.MapGet("/{id}", GetUserById)
                .WithName("GetUserById")
                .WithSummary("Get a user by ID");

            group.MapGet("/", GetAllUsers)
                .WithName("GetAllUsers")
                .WithSummary("Get all users");

            group.MapPut("/{id}", UpdateUser)
                .WithName("UpdateUser")
                .WithSummary("Update an existing user");

            group.MapDelete("/{id}", DeleteUser)
                .WithName("DeleteUser")
                .WithSummary("Delete a user");

            group.MapGet("/stats/total-count", GetTotalUserCount)
                .WithName("GetTotalUserCount")
                .WithSummary("Get the total number of users");

            group.MapGet("/stats/users-per-group", GetUsersPerGroup)
                .WithName("GetUsersPerGroup")
                .WithSummary("Get the number of users per group");
        }

        private static async Task<Created<UserResponse>> CreateUser(
            CreateUserRequest request,
            IUserService userService)
        {
            var user = await userService.CreateUserAsync(request);
            return TypedResults.Created($"/api/users/{user.Id}", user);
        }

        private static async Task<Ok<UserResponse>> GetUserById(
            int id,
            IUserService userService)
        {
            var user = await userService.GetUserByIdAsync(id);
            if (user == null)
                return TypedResults.Ok(new UserResponse());

            return TypedResults.Ok(user);
        }

        private static async Task<Ok<List<UserResponse>>> GetAllUsers(
            IUserService userService)
        {
            var users = await userService.GetAllUsersAsync();
            return TypedResults.Ok(users);
        }

        private static async Task<Ok<UserResponse>> UpdateUser(
            int id,
            UpdateUserRequest request,
            IUserService userService)
        {
            var user = await userService.UpdateUserAsync(id, request);
            if (user == null)
                return TypedResults.Ok(new UserResponse());

            return TypedResults.Ok(user);
        }

        private static async Task<Ok<bool>> DeleteUser(
            int id,
            IUserService userService)
        {
            var success = await userService.DeleteUserAsync(id);
            return TypedResults.Ok(success);
        }

        private static async Task<Ok<int>> GetTotalUserCount(
            IUserService userService)
        {
            var count = await userService.GetTotalUserCountAsync();
            return TypedResults.Ok(count);
        }

        private static async Task<Ok<List<UsersPerGroupResponse>>> GetUsersPerGroup(
            IUserService userService)
        {
            var stats = await userService.GetUsersPerGroupAsync();
            return TypedResults.Ok(stats);
        }
    }
}
