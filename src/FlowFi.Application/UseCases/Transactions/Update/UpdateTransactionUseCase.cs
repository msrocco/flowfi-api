using AutoMapper;
using FlowFi.Communication.Requests;
using FlowFi.Domain.Repositories;
using FlowFi.Domain.Repositories.Transaction;
using FlowFi.Domain.Services.LoggedUser;
using FlowFi.Exception;
using FlowFi.Exception.ExceptionsBase;

namespace FlowFi.Application.UseCases.Transactions.Update;

public class UpdateTransactionUseCase : IUpdateTransactionUseCase
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITransactionUpdateOnlyRepository _repository;
    private readonly ILoggedUser _loggedUser;

    public UpdateTransactionUseCase(
        IMapper mapper,
        IUnitOfWork unitOfWork,
        ITransactionUpdateOnlyRepository repository,
        ILoggedUser loggedUser)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _repository = repository;
        _loggedUser = loggedUser;
    }

    public async Task Execute(Guid id, RequestTransactionJson request)
    {
        Validate(request);

        var loggedUser = await _loggedUser.Get();

        var transaction = await _repository.GetById(loggedUser, id);

        if (transaction is null)
        {
            throw new NotFoundException(ResourceErrorMessages.BANK_ACCOUNT_NOT_FOUND);
        }

        _mapper.Map(request, transaction);

        _repository.Update(transaction);

        await _unitOfWork.Commit();
    }

    private void Validate(RequestTransactionJson request)
    {
        var validator = new TransactionValidator();

        var result = validator.Validate(request);

        if (result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
