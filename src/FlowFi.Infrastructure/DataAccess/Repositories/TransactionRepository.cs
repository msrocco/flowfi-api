using FlowFi.Domain.Entities;
using FlowFi.Domain.Repositories.Transaction;
using Microsoft.EntityFrameworkCore;

namespace FlowFi.Infrastructure.DataAccess.Repositories;

internal class TransactionRepository : ITransactionWriteOnlyRepository, ITransactionReadOnlyRepository
{
    private readonly FlowFiDbContext _dbContext;

    public TransactionRepository(FlowFiDbContext dbContext) => _dbContext = dbContext;

    public async Task Add(Transaction transaction)
    {
        await _dbContext.Transactions.AddAsync(transaction);
    }

    public async Task<List<Transaction>> GetAll(User user)
    {
        return await _dbContext.Transactions
            .AsNoTracking()
            .Where(transaction => transaction.UserId == user.Id)
            .ToListAsync();
    }

    public async Task<Transaction?> GetById(User user, Guid id)
    {
        return await _dbContext
            .Transactions
            .AsNoTracking()
            .FirstOrDefaultAsync(transaction => transaction.Id == id && transaction.UserId == user.Id);
    }
}
