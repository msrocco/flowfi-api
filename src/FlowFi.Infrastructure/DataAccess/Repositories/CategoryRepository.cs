using FlowFi.Domain.Entities;
using FlowFi.Domain.Repositories.Category;

namespace FlowFi.Infrastructure.DataAccess.Repositories;

internal class CategoryRepository : ICategoryWriteOnlyRepository
{
    private readonly FlowFiDbContext _dbContext;

    public CategoryRepository(FlowFiDbContext dbContext) => _dbContext = dbContext;

    public async Task Add(Category category)
    {
        await _dbContext.Categories.AddAsync(category);
    }
}
