namespace FlowFi.Domain.Repositories.BankAccount;

public interface IBankAccountReadOnlyRepository
{
    Task<List<Entities.BankAccount>> GetAll(Entities.User user);
    Task<Entities.BankAccount?> GetById(Entities.User user, Guid id);
}
