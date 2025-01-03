using AutoMapper;
using FlowFi.Communication.Responses;
using FlowFi.Domain.Repositories.BankAccount;
using FlowFi.Domain.Services.LoggedUser;

namespace FlowFi.Application.UseCases.BankAccounts.GetAll;

public class GetAllBankAccountsUseCase : IGetAllBankAccountsUseCase
{
    private readonly IBankAccountReadOnlyRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILoggedUser _loggedUser;

    public GetAllBankAccountsUseCase(IBankAccountReadOnlyRepository repository, IMapper mapper, ILoggedUser loggedUser)
    {
        _repository = repository;
        _mapper = mapper;
        _loggedUser = loggedUser;
    }

    public async Task<ResponseBankAccountsJson> Execute()
    {
        var loggedUser = await _loggedUser.Get();

        var result = await _repository.GetAll(loggedUser);

        return new ResponseBankAccountsJson
        {
            BankAccounts = _mapper.Map<List<ResponseShortBankAccountsJson>>(result)
        };
    }
}
