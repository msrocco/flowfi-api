using FlowFi.Domain.Repositories;
using FlowFi.Domain.Services.LoggedUser;
using FlowFi.Domain.Repositories.Transaction;
using FlowFi.Exception.ExceptionsBase;
using FlowFi.Exception;

namespace FlowFi.Application.UseCases.Transactions.Delete;

public class DeleteTransactionUseCase : IDeleteTransactionUseCase
{
    private readonly ITransactionWriteOnlyRepository _writeOnlyRepository;
    private readonly ITransactionReadOnlyRepository _readOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILoggedUser _loggedUser;

    public DeleteTransactionUseCase(
        ITransactionWriteOnlyRepository writeOnlyRepository,
        ITransactionReadOnlyRepository readOnlyRepository,
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

        _ = await _readOnlyRepository.GetById(loggedUser, id)
            ?? throw new NotFoundException(ResourceErrorMessages.TRANSACTION_NOT_FOUND);

        await _writeOnlyRepository.Delete(id);

        await _unitOfWork.Commit();
    }
}
