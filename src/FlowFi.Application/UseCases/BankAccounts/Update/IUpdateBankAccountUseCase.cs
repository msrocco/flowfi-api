using FlowFi.Communication.Requests;

namespace FlowFi.Application.UseCases.BankAccounts.Update;
public interface IUpdateBankAccountUseCase
{
    Task Execute(Guid id, RequestBankAccountJson request);
}
