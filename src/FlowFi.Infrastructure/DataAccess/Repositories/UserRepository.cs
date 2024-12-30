using FlowFi.Domain.Entities;
using FlowFi.Domain.Repositories.User;
using FlowFi.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace FlowFi.Infrastructure.DataAccess.Repositories;

internal class UserRepository : IUserReadOnlyRepository, IUserWriteOnlyRepository, IUserUpdateOnlyRepository
{
    private readonly FlowFiDbContext _dbContext;

    public UserRepository(FlowFiDbContext dbContext) => _dbContext = dbContext;

    public async Task Add(User user)
    {
        await _dbContext.Users.AddAsync(user);
    }

    public async Task<bool> ExistActiveUserWithEmail(string email)
    {
        return await _dbContext.Users.AnyAsync(user => user.Email.Equals(email));
    }

    public async Task<User> GetById(Guid id)
    {
        return await _dbContext.Users.FirstAsync(user => user.Id == id);
    }

    public async Task<User?> GetUserByEmail(string email)
    {
        return await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(user => user.Email.Equals(email));
    }
}
