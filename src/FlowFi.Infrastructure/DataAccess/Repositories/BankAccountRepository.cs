using FlowFi.Domain.Entities;
using FlowFi.Domain.Repositories.Expenses;

namespace FlowFi.Infrastructure.DataAccess.Repositories;

internal class BankAccountRepository : IBankAccountWriteOnlyRepository
{
    private readonly FlowFiDbContext _dbContext;

    public BankAccountRepository(FlowFiDbContext dbContext) => _dbContext = dbContext;

    public async Task Add(BankAccount bankAccount)
    {
        await _dbContext.BankAccounts.AddAsync(bankAccount);
    }
}
