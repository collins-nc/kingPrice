namespace KingPrice.Web
{
    using KingPrice.Abstraction.Dtos;


    public class UserApiClient(HttpClient httpClient, ILogger<UserApiClient> logger) : IUserApiClient
    {

        public async Task<UserResponse?> CreateUserAsync(CreateUserRequest request)
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync("/api/users", request);
                
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<UserResponse>();
                }

                logger.LogError($"Failed to create user. Status: {response.StatusCode}");
                return null;
            }
            catch (Exception ex)
            {
                logger.LogError($"Error creating user: {ex.Message}");
                throw;
            }
        }

        public async Task<UserResponse?> GetUserByIdAsync(int id)
        {
            try
            {
                var response = await httpClient.GetAsync($"/api/users/{id}");
                
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<UserResponse>();
                }

                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    logger.LogInformation($"User with ID {id} not found.");
                    return null;
                }

                logger.LogError($"Failed to get user {id}. Status: {response.StatusCode}");
                return null;
            }
            catch (Exception ex)
            {
                logger.LogError($"Error getting user {id}: {ex.Message}");
                throw;
            }
        }

        public async Task<List<UserResponse>> GetAllUsersAsync()
        {
            try
            {
                var response = await httpClient.GetAsync("/api/users");
                
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<UserResponse>>() ?? new List<UserResponse>();
                }

                logger.LogError($"Failed to get all users. Status: {response.StatusCode}");
                return new List<UserResponse>();
            }
            catch (Exception ex)
            {
                logger.LogError($"Error getting all users: {ex.Message}");
                throw;
            }
        }

        public async Task<UserResponse?> UpdateUserAsync(int id, UpdateUserRequest request)
        {
            try
            {
                var response = await httpClient.PutAsJsonAsync($"/api/users/{id}", request);
                
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<UserResponse>();
                }

                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    logger.LogInformation($"User with ID {id} not found for update.");
                    return null;
                }

                logger.LogError($"Failed to update user {id}. Status: {response.StatusCode}");
                return null;
            }
            catch (Exception ex)
            {
                logger.LogError($"Error updating user {id}: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            try
            {
                var response = await httpClient.DeleteAsync($"/api/users/{id}");
                
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<bool>();
                }

                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    logger.LogInformation($"User with ID {id} not found for deletion.");
                    return false;
                }

                logger.LogError($"Failed to delete user {id}. Status: {response.StatusCode}");
                return false;
            }
            catch (Exception ex)
            {
                logger.LogError($"Error deleting user {id}: {ex.Message}");
                throw;
            }
        }

        public async Task<int> GetTotalUserCountAsync()
        {
            try
            {
                var response = await httpClient.GetAsync("/api/users/stats/total-count");
                
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<int>();
                }

                logger.LogError($"Failed to get total user count. Status: {response.StatusCode}");
                return 0;
            }
            catch (Exception ex)
            {
                logger.LogError($"Error getting total user count: {ex.Message}");
                throw;
            }
        }

        public async Task<List<UsersPerGroupResponse>> GetUsersPerGroupAsync()
        {
            try
            {
                var response = await httpClient.GetAsync("/api/users/stats/users-per-group");
                
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<UsersPerGroupResponse>>() ?? new List<UsersPerGroupResponse>();
                }

                logger.LogError($"Failed to get users per group. Status: {response.StatusCode}");
                return new List<UsersPerGroupResponse>();
            }
            catch (Exception ex)
            {
                logger.LogError($"Error getting users per group: {ex.Message}");
                throw;
            }
        }
    }
}