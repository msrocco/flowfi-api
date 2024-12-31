namespace FlowFi.Domain.Entities;

public class BankAccount
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public float InitialBalance { get; set; }
    public string Type { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;

    public Guid UserId { get; set; }
    public User User { get; set; } = default!;
}
