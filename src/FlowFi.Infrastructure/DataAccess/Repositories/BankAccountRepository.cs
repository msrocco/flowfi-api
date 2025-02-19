﻿using FlowFi.Domain.Entities;
using FlowFi.Domain.Repositories.BankAccount;
using Microsoft.EntityFrameworkCore;

namespace FlowFi.Infrastructure.DataAccess.Repositories;

internal class BankAccountRepository : IBankAccountWriteOnlyRepository, IBankAccountReadOnlyRepository, IBankAccountUpdateOnlyRepository
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

     async Task<BankAccount?> IBankAccountReadOnlyRepository.GetById(User user, Guid id)
    {
        return await _dbContext
            .BankAccounts
            .AsNoTracking()
            .FirstOrDefaultAsync(bankAccount => bankAccount.Id == id && bankAccount.UserId == user.Id);
    }

    async Task<BankAccount?> IBankAccountUpdateOnlyRepository.GetById(User user, Guid id)
    {
        return await _dbContext
            .BankAccounts
            .FirstOrDefaultAsync(bankAccount => bankAccount.Id == id && bankAccount.UserId == user.Id);
    }

    public void Update(BankAccount bankAccount)
    {
        _dbContext.BankAccounts.Update(bankAccount);
    }

    public async Task Delete(Guid id)
    {
        var result = await _dbContext.BankAccounts.FindAsync(id);

        _dbContext.BankAccounts.Remove(result!);
    }
}
