using AutoMapper;
using FlowFi.Communication.Responses;
using FlowFi.Domain.Repositories.BankAccount;
using FlowFi.Domain.Services.LoggedUser;
using FlowFi.Exception.ExceptionsBase;
using FlowFi.Exception;

namespace FlowFi.Application.UseCases.BankAccounts.GetById;

public class GetBankAccountByIdUseCase : IGetBankAccountByIdUseCase
{
    private readonly IBankAccountReadOnlyRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILoggedUser _loggedUser;

    public GetBankAccountByIdUseCase(IBankAccountReadOnlyRepository repository, IMapper mapper, ILoggedUser loggedUser)
    {
        _repository = repository;
        _mapper = mapper;
        _loggedUser = loggedUser;
    }

    public async Task<ResponseShortBankAccountsJson> Execute(Guid id)
    {
        var loggedUser = await _loggedUser.Get();

        var result = await _repository.GetById(loggedUser, id);

        if (result is null)
        {
            throw new NotFoundException(ResourceErrorMessages.BANK_ACCOUNT_NOT_FOUND);
        }

        return _mapper.Map<ResponseShortBankAccountsJson>(result);
    }
}
