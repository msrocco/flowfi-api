using FlowFi.Domain.Entities;
using FlowFi.Domain.Repositories.Category;
using Microsoft.EntityFrameworkCore;

namespace FlowFi.Infrastructure.DataAccess.Repositories;

internal class CategoryRepository : ICategoryWriteOnlyRepository, ICategoryReadOnlyRepository
{
    private readonly FlowFiDbContext _dbContext;

    public CategoryRepository(FlowFiDbContext dbContext) => _dbContext = dbContext;

    public async Task Add(Category category)
    {
        await _dbContext.Categories.AddAsync(category);
    }

    public async Task<List<Category>> GetAll(User user)
    {
        return await _dbContext.Categories
            .AsNoTracking()
            .Where(category => category.UserId == user.Id)
            .ToListAsync();
    }

    public async Task<Category?> GetById(User user, Guid id)
    {
        return await _dbContext
            .Categories
            .AsNoTracking()
            .FirstOrDefaultAsync(category => category.Id == id && category.UserId == user.Id);
    }

    public async Task Delete(Guid id)
    {
        var result = await _dbContext.Categories.FindAsync(id);

        _dbContext.Categories.Remove(result!);
    }
}
