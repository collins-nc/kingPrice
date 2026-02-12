namespace KingPrice.Abstraction.Dtos
{
    public class CreateUserRequest
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public List<string?> GroupNames { get; set; } = [];
    }
}
