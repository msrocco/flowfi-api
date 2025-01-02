namespace FlowFi.Domain.Entities;

public class Category
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }

    public string Name { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;

    public User User { get; set; } = default!;
    public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
