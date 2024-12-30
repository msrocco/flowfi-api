using FlowFi.Domain.Entities;

namespace FlowFi.Domain.Security.Tokens;
public interface IAccessTokenGenerator
{
    string Generate(User user);
}
