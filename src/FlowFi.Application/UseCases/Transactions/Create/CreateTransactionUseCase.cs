using AutoMapper;
using FlowFi.Communication.Requests;
using FlowFi.Domain.Entities;
using FlowFi.Domain.Repositories;
using FlowFi.Domain.Repositories.Transaction;
using FlowFi.Domain.Services.LoggedUser;

namespace FlowFi.Application.UseCases.Transactions.Create;

public class CreateTransactionUseCase : ICreateTransactionUseCase
{
    private readonly ITransactionWriteOnlyRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILoggedUser _loggedUser;

    public CreateTransactionUseCase(
        ITransactionWriteOnlyRepository repository,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ILoggedUser loggedUser)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _loggedUser = loggedUser;
    }

    public async Task Execute(RequestTransactionJson request)
    {
        Validate(request);

        var loggedUser = await _loggedUser.Get();

        var transaction = _mapper.Map<Transaction>(request);
        transaction.UserId = loggedUser.Id;
        transaction.Date = DateTime.UtcNow;

        await _repository.Add(transaction);

        await _unitOfWork.Commit();
    }

    private void Validate(RequestTransactionJson request)
    {
        return;
    }
}
