using AutoMapper;
using FlowFi.Communication.Responses;
using FlowFi.Communication.Requests;
using FlowFi.Domain.Entities;

namespace FlowFi.Application.AutoMapper;

public class AutoMapping : Profile
{
    public AutoMapping()
    {
        RequestToEntity();
        EntityToResponse();
    }
    
    private void RequestToEntity()
    {
        CreateMap<RequestSignUpJson, User>()
            .ForMember(dest => dest.Password, config => config.Ignore());

        CreateMap<RequestBankAccountJson, BankAccount>();
    }

    private void EntityToResponse()
    {
        CreateMap<User, ResponseUserProfileJson>();
        CreateMap<BankAccount, ResponseCreatedBankAccountJson>();
    }
}
