using FlowFi.Application.UseCases.Users.Profile;
using FlowFi.Communication.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlowFi.Api.Controllers;

[Route("users")]
[ApiController]
public class UsersController : ControllerBase
{
    [HttpGet("me")]
    [Authorize]
    [ProducesResponseType(typeof(ResponseUserProfileJson), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetProfile([FromServices] IGetUserProfileUseCase useCase)
    {
        var response = await useCase.Execute();

        return Ok(response);
    }
}
