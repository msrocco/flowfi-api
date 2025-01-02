namespace FlowFi.Domain.Repositories.Category;

public interface ICategoryWriteOnlyRepository
{
    Task Add(Entities.Category category);
}
