using FlowFi.Communication.Responses;

namespace FlowFi.Application.UseCases.BankAccounts.GetById;

public interface IGetBankAccountByIdUseCase
{
    Task<ResponseShortBankAccountsJson> Execute(Guid id);
}
