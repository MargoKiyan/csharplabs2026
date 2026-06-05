using AsyncDataLibrary.Attributes;
using AsyncDataLibrary.Interfaces;

namespace AsyncDataLibrary.Models;

[DataFile("orders.json")]
public class Order : IEntity
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid BookId { get; set; }
    public DateTime OrderDate { get; set; }
    public string Status { get; set; } = string.Empty;
}
