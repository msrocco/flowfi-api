namespace FlowFi.Domain.Repositories.Category;

public interface ICategoryReadOnlyRepository
{
    Task<List<Entities.Category>> GetAll(Entities.User user);
    Task<Entities.Category?> GetById(Entities.User user, Guid id);
}
