using AutoMapper;
using FlowFi.Communication.Responses;
using FlowFi.Domain.Repositories.Transaction;
using FlowFi.Domain.Services.LoggedUser;

namespace FlowFi.Application.UseCases.Transactions.GetAll;

public class GetAllTransactionsUseCase : IGetAllTransactionsUseCase
{
    private readonly ITransactionReadOnlyRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILoggedUser _loggedUser;

    public GetAllTransactionsUseCase(ITransactionReadOnlyRepository repository, IMapper mapper, ILoggedUser loggedUser)
    {
        _repository = repository;
        _mapper = mapper;
        _loggedUser = loggedUser;
    }

    public async Task<ResponseTransactionsJson> Execute(int? month = null, int? year = null, Guid? bankAccountId = null, string? type = null)
    {
        var loggedUser = await _loggedUser.Get();

        var result = await _repository.GetAll(loggedUser, month, year, bankAccountId, type);

        return new ResponseTransactionsJson
        {
            Transactions = _mapper.Map<List<ResponseShortTransactionJson>>(result)
        };
    }
}
