using AsyncDataLibrary.Attributes;
using AsyncDataLibrary.Interfaces;

namespace AsyncDataLibrary.Models;

[DataFile("users.json")]
public class User : IEntity
{
    public Guid Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime RegisteredAt { get; set; }
}
