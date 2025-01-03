using FlowFi.Communication.Responses;

namespace FlowFi.Application.UseCases.BankAccounts.GetAll;

public interface IGetAllBankAccountsUseCase
{
    Task<ResponseBankAccountsJson> Execute();
}
