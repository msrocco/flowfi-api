using FlowFi.Communication.Requests;
using FlowFi.Communication.Responses;

namespace FlowFi.Application.UseCases.Auth.SignIn;
public interface ISignInUseCase
{
    Task<ResponseSignUpJson> Execute(RequestSignInJson request);
}
