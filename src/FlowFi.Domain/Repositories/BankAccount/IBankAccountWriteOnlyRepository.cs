namespace FlowFi.Domain.Repositories.Expenses;

public interface IBankAccountWriteOnlyRepository
{
    Task Add(Entities.BankAccount bankAccount);

    Task Delete(Guid id);
}
