namespace FlowFi.Domain.Repositories.Transaction;

public interface ITransactionReadOnlyRepository
{
    Task<List<Entities.Transaction>> GetAll(Entities.User user);
    Task<Entities.Transaction?> GetById(Entities.User user, Guid id);
}
