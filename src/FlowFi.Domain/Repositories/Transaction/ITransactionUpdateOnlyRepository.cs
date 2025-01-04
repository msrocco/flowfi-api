namespace FlowFi.Domain.Repositories.Transaction;
public interface ITransactionUpdateOnlyRepository
{
    Task<Entities.Transaction?> GetById(Entities.User user, Guid id);
    void Update(Entities.Transaction transaction);
}
