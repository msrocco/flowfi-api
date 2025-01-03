using FlowFi.Domain.Entities;
using FlowFi.Domain.Repositories.BankAccount;
using FlowFi.Domain.Repositories.Expenses;
using Microsoft.EntityFrameworkCore;

namespace FlowFi.Infrastructure.DataAccess.Repositories;

internal class BankAccountRepository : IBankAccountWriteOnlyRepository, IBankAccountReadOnlyRepository
{
    private readonly FlowFiDbContext _dbContext;

    public BankAccountRepository(FlowFiDbContext dbContext) => _dbContext = dbContext;

    public async Task Add(BankAccount bankAccount)
    {
        await _dbContext.BankAccounts.AddAsync(bankAccount);
    }

    public async Task<List<BankAccount>> GetAll(User user)
    {
        return await _dbContext.BankAccounts.AsNoTracking().Where(bankAccount => bankAccount.UserId == user.Id).ToListAsync();
    }

    public async Task<BankAccount?> GetById(User user, Guid id)
    {
        return await _dbContext
            .BankAccounts
            .AsNoTracking()
            .FirstOrDefaultAsync(bankAccount => bankAccount.Id == id && bankAccount.UserId == user.Id);
    }

    public async Task Delete(Guid id)
    {
        var result = await _dbContext.BankAccounts.FindAsync(id);

        _dbContext.BankAccounts.Remove(result!);
    }
}
