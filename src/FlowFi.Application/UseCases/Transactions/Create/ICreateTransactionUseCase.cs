using FlowFi.Communication.Requests;

namespace FlowFi.Application.UseCases.Transactions.Create;

public interface ICreateTransactionUseCase
{
    Task Execute(RequestTransactionJson request);
}
