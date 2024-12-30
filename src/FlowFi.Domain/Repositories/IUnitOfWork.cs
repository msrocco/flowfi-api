namespace FlowFi.Domain.Repositories;
public interface IUnitOfWork
{
    Task Commit();
}
