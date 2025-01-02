namespace FlowFi.Domain.Entities;

public class Transaction
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid BankAccountId { get; set; }
    public Guid? CategoryId { get; set; }

    public string Name { get; set; } = string.Empty;
    public float Value { get; set; }
    public DateTime Date { get; set; }
    public string Type { get; set; } = string.Empty;

    public User User { get; set; } = default!;
    public BankAccount BankAccount { get; set; } = default!;
    public Category Category { get; set; } = default!;
}
