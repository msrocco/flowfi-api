namespace FlowFi.Application.UseCases.Transactions.Delete;

public interface IDeleteTransactionUseCase
{
    Task Execute(Guid id);
}
