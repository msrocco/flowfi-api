namespace FlowFi.Application.UseCases.Categories.Delete;

public interface IDeleteCategoryUseCase
{
    Task Execute(Guid id);
}
