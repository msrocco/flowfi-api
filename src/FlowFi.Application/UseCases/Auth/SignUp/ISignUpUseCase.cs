using FlowFi.Communication.Requests;
using FlowFi.Communication.Responses;

namespace FlowFi.Application.UseCases.Users.Register;
public interface ISignUpUseCase
{
    Task<ResponseSignUpJson> Execute(RequestSignUpJson request);
}
