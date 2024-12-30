using FlowFi.Application.UseCases.Auth.SignIn;
using FlowFi.Application.UseCases.Users.Register;
using FlowFi.Communication.Requests;
using FlowFi.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace FlowFi.Api.Controllers;

[Route("auth")]
[ApiController]
public class AuthController : ControllerBase
{
    [HttpPost("signin")]
    [ProducesResponseType(typeof(ResponseSignUpJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> SingIn(
    [FromServices] ISignInUseCase useCase,
    [FromBody] RequestSignInJson request)
    {
        var response = await useCase.Execute(request);
        return Ok(response);
    }

    [HttpPost("signup")]
    [ProducesResponseType(typeof(ResponseSignUpJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register(
    [FromServices] ISignUpUseCase useCase,
    [FromBody] RequestSignUpJson request)
    {
        var response = await useCase.Execute(request);

        return Created(string.Empty, response);
    }
}
