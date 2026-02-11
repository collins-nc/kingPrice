namespace KingPrice.Core.Entities;

using System.ComponentModel.DataAnnotations;

public class Group
{
    public int Id { get; set; }

    [MaxLength(256)]
    public string Name { get; set; } = null!;

    public List<User> Users { get; set; } = [];

    public List<Permission> Permissions { get; set; } = [];
}
