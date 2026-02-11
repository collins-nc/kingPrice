namespace KingPrice.Core.Entities;

using System.ComponentModel.DataAnnotations;

public class User
{
    public int Id { get; set; }
    
    [MaxLength(256)]
    public string FirstName { get; set; } = null!;

    [MaxLength(256)]
    public string LastName { get; set; } = null!;

    public List<Group> Groups { get; set; } = [];
}
