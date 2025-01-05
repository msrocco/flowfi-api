namespace FlowFi.Domain.Repositories.Transaction;

public interface ITransactionReadOnlyRepository
{
    Task<List<Entities.Transaction>> GetAll(Entities.User user, int? month = null, int? year = null, Guid? bankAccountId = null, string? type = null);
    Task<Entities.Transaction?> GetById(Entities.User user, Guid id);
}
