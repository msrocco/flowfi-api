using AutoMapper;
using FlowFi.Communication.Requests;
using FlowFi.Domain.Repositories;
using FlowFi.Domain.Repositories.BankAccount;
using FlowFi.Domain.Services.LoggedUser;
using FlowFi.Exception;
using FlowFi.Exception.ExceptionsBase;

namespace FlowFi.Application.UseCases.BankAccounts.Update;

public class UpdateBankAccountUseCase : IUpdateBankAccountUseCase
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBankAccountUpdateOnlyRepository _repository;
    private readonly ILoggedUser _loggedUser;

    public UpdateBankAccountUseCase(
        IMapper mapper,
        IUnitOfWork unitOfWork,
        IBankAccountUpdateOnlyRepository repository,
        ILoggedUser loggedUser)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _repository = repository;
        _loggedUser = loggedUser;
    }

    public async Task Execute(Guid id, RequestBankAccountJson request)
    {
        Validate(request);

        var loggedUser = await _loggedUser.Get();

        var expense = await _repository.GetById(loggedUser, id);

        if (expense is null)
        {
            throw new NotFoundException(ResourceErrorMessages.BANK_ACCOUNT_NOT_FOUND);
        }

        _mapper.Map(request, expense);

        _repository.Update(expense);

        await _unitOfWork.Commit();
    }

    private void Validate(RequestBankAccountJson request)
    {
        var validator = new BankAccountValidator();

        var result = validator.Validate(request);

        if (result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
