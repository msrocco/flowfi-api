using FlowFi.Domain.Entities;
using FlowFi.Domain.Repositories.Transaction;
using Microsoft.EntityFrameworkCore;

namespace FlowFi.Infrastructure.DataAccess.Repositories;

internal class TransactionRepository : ITransactionWriteOnlyRepository
{
    private readonly FlowFiDbContext _dbContext;

    public TransactionRepository(FlowFiDbContext dbContext) => _dbContext = dbContext;

    public async Task Add(Transaction transaction)
    {
        await _dbContext.Transactions.AddAsync(transaction);
    }
}
