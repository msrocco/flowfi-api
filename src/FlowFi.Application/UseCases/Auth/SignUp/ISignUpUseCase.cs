using FlowFi.Communication.Requests;
using FlowFi.Communication.Responses;

namespace FlowFi.Application.UseCases.Auth.SignUp;

public interface ISignUpUseCase
{
    Task<ResponseSignUpJson> Execute(RequestSignUpJson request);
}
