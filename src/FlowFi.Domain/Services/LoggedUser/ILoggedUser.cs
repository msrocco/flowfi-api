using FlowFi.Domain.Entities;

namespace FlowFi.Domain.Services.LoggedUser;
public interface ILoggedUser
{
    Task<User> Get();
}
