namespace KingPrice.Core.DTOs
{
    public class UserResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public List<int> GroupIds { get; set; } = [];
    }
}
