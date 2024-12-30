using FlowFi.Domain.Repositories;

namespace FlowFi.Infrastructure.DataAccess;

internal class UnitOfWork : IUnitOfWork
{
    private readonly FlowFiDbContext _dbContext;

    public UnitOfWork(FlowFiDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Commit() => await _dbContext.SaveChangesAsync();
}
