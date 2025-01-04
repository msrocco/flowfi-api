namespace FlowFi.Domain.Repositories.Category;

public interface ICategoryWriteOnlyRepository
{
    Task Add(Entities.Category category);
    Task AddRange(IEnumerable<Entities.Category> categories);
    Task Delete(Guid id);
}
