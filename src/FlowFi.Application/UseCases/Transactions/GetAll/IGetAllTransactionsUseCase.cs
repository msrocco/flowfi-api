using FlowFi.Communication.Responses;

namespace FlowFi.Application.UseCases.Transactions.GetAll;

public interface IGetAllTransactionsUseCase
{
    Task<ResponseTransactionsJson> Execute();
}
