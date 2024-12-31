using AutoMapper;
using FlowFi.Domain.Repositories.Expenses;
using FlowFi.Communication.Requests;
using FlowFi.Domain.Repositories;
using FlowFi.Domain.Services.LoggedUser;
using FlowFi.Domain.Entities;
using FlowFi.Communication.Responses;

namespace FlowFi.Application.UseCases.BankAccounts.Create;

public class CreateBankAccountUseCase : ICreateBankAccountUseCase
{
    private readonly IBankAccountWriteOnlyRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILoggedUser _loggedUser;

    public CreateBankAccountUseCase(IBankAccountWriteOnlyRepository repository, IUnitOfWork unitOfWork, IMapper mapper, ILoggedUser loggedUser)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _loggedUser = loggedUser;
    }

    public async Task<ResponseCreatedBankAccountJson> Execute(RequestBankAccountJson request)
    {
        var loggedUser = await _loggedUser.Get();

        var bankAccount = _mapper.Map<BankAccount>(request);
        bankAccount.UserId = loggedUser.Id;

        await _repository.Add(bankAccount);

        await _unitOfWork.Commit();

        return _mapper.Map<ResponseCreatedBankAccountJson>(bankAccount);
    }

    private void Validate(RequestBankAccountJson request)
    {
        return;
    }
}
