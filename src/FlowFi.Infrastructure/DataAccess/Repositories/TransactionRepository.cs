using FlowFi.Domain.Entities;
using FlowFi.Domain.Repositories.Transaction;
using FlowFi.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace FlowFi.Infrastructure.DataAccess.Repositories;

internal class TransactionRepository : ITransactionWriteOnlyRepository, ITransactionReadOnlyRepository, ITransactionUpdateOnlyRepository
{
    private readonly FlowFiDbContext _dbContext;

    public TransactionRepository(FlowFiDbContext dbContext) => _dbContext = dbContext;

    public async Task Add(Transaction transaction)
    {
        await _dbContext.Transactions.AddAsync(transaction);
    }

    public async Task<List<Transaction>> GetAll(
        User user,
        int? month = null,
        int? year = null,
        Guid? bankAccountId = null,
        string? type = null)
    {
        return await _dbContext.Transactions
            .AsNoTracking()
            .Where(transaction => transaction.UserId == user.Id)
            .ApplyFilter(month.HasValue, transaction => transaction.Date.Month == month.GetValueOrDefault())
            .ApplyFilter(year.HasValue, transaction => transaction.Date.Year == year.GetValueOrDefault())
            .ApplyFilter(bankAccountId.HasValue, transaction => transaction.BankAccountId == bankAccountId)
            .ApplyFilter(!string.IsNullOrEmpty(type), transaction => transaction.Type == type)
            .ToListAsync();
    }

    async Task<Transaction?> ITransactionReadOnlyRepository.GetById(User user, Guid id)
    {
        return await _dbContext
            .Transactions
            .AsNoTracking()
            .FirstOrDefaultAsync(transaction => transaction.Id == id && transaction.UserId == user.Id);
    }

    async Task<Transaction?> ITransactionUpdateOnlyRepository.GetById(User user, Guid id)
    {
        return await _dbContext
            .Transactions
            .FirstOrDefaultAsync(transaction => transaction.Id == id && transaction.UserId == user.Id);
    }

    public void Update(Transaction transaction)
    {
        _dbContext.Transactions.Update(transaction);
    }

    public async Task Delete(Guid id)
    {
        var result = await _dbContext.Transactions.FindAsync(id);

        _dbContext.Transactions.Remove(result!);
    }
}
