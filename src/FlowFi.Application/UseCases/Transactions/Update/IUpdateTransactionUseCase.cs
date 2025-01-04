using FlowFi.Communication.Requests;

namespace FlowFi.Application.UseCases.Transactions.Update;

public interface IUpdateTransactionUseCase
{
    Task Execute(Guid id, RequestTransactionJson request);
}
