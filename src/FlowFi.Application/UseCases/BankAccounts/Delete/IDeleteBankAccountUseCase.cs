namespace FlowFi.Application.UseCases.BankAccounts.Delete;

public interface IDeleteBankAccountUseCase
{
    Task Execute(Guid id);
}
