namespace FlowFi.Domain.Repositories.BankAccount;
public interface IBankAccountUpdateOnlyRepository
{
    Task<Entities.BankAccount?> GetById(Entities.User user, Guid id);
    void Update(Entities.BankAccount bankAccount);
}
