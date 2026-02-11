namespace KingPrice.Core;

using DTOs;

public interface IUserService
{
    Task<UserResponse> CreateUserAsync(CreateUserRequest request);
    Task<UserResponse?> GetUserByIdAsync(int id);
    Task<List<UserResponse>> GetAllUsersAsync();
    Task<UserResponse?> UpdateUserAsync(int id, UpdateUserRequest request);
    Task<bool> DeleteUserAsync(int id);
    Task<int> GetTotalUserCountAsync();
    Task<List<UsersPerGroupResponse>> GetUsersPerGroupAsync();
}
