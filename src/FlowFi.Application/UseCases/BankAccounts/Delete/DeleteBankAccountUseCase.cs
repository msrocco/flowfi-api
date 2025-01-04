
using FlowFi.Domain.Repositories;
using FlowFi.Domain.Repositories.BankAccount;
using FlowFi.Domain.Services.LoggedUser;
using FlowFi.Exception.ExceptionsBase;

namespace FlowFi.Application.UseCases.BankAccounts.Delete;

public class DeleteBankAccountUseCase : IDeleteBankAccountUseCase
{
    private readonly IBankAccountWriteOnlyRepository _writeOnlyRepository;
    private readonly IBankAccountReadOnlyRepository _readOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILoggedUser _loggedUser;

    public DeleteBankAccountUseCase(
        IBankAccountWriteOnlyRepository writeOnlyRepository,
        IBankAccountReadOnlyRepository readOnlyRepository,
        IUnitOfWork unitOfWork,
        ILoggedUser loggedUser)
    {
        _writeOnlyRepository = writeOnlyRepository;
        _readOnlyRepository = readOnlyRepository;
        _unitOfWork = unitOfWork;
        _loggedUser = loggedUser;
    }

    public async Task Execute(Guid id)
    {
        var loggedUser = await _loggedUser.Get();

        var bankAccount = await _readOnlyRepository.GetById(loggedUser, id) 
            ?? throw new NotFoundException("Bank account not found.");

        await _writeOnlyRepository.Delete(id);

        await _unitOfWork.Commit();
    }
}
