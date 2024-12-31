using FlowFi.Application.UseCases.BankAccounts.Create;
using FlowFi.Communication.Requests;
using FlowFi.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace FlowFi.Api.Controllers;

[Route("bank-accounts")]
[ApiController]
public class BankAccountsController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseCreatedBankAccountJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(
        [FromServices] ICreateBankAccountUseCase useCase,
        [FromBody] RequestBankAccountJson request)
    {
        var response = await useCase.Execute(request);

        return Created(string.Empty, response);
    }
}
