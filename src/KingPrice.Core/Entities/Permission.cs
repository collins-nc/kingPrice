namespace KingPrice.Core.Entities;

using System.ComponentModel.DataAnnotations;

public class Permission
{
    public int Id { get; set; }
    
    [MaxLength(256)]
    public string Name { get; set; } = null!;

    public List<Group> Groups { get; set; } = [];
}
