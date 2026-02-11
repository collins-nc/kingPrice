namespace KingPrice.Core.DTOs
{
    public class UsersPerGroupResponse
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; } = null!;
        public int UserCount { get; set; }
    }
}
