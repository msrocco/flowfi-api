using FlowFi.Communication.Responses;

namespace FlowFi.Application.UseCases.Users.Profile;

public interface IGetUserProfileUseCase
{
    Task<ResponseUserProfileJson> Execute();
}
