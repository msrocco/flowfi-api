using FlowFi.Communication.Requests;
using FlowFi.Communication.Responses;

namespace FlowFi.Application.UseCases.BankAccounts.Create;

public interface ICreateBankAccountUseCase
{
    Task<ResponseCreatedBankAccountJson> Execute(RequestBankAccountJson request);
}
