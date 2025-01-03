using FlowFi.Application.UseCases.BankAccounts.Create;
using FlowFi.Application.UseCases.BankAccounts.Delete;
using FlowFi.Application.UseCases.BankAccounts.GetAll;
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

    [HttpGet]
    [ProducesResponseType(typeof(ResponseBankAccountsJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetAllBankAccounts(
        [FromServices] IGetAllBankAccountsUseCase useCase)
    {
        var response = await useCase.Execute();

        if (response.BankAccounts.Count != 0)
            return Ok(response);

        return NoContent();
    }

    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(
        [FromServices] IDeleteBankAccountUseCase useCase,
        [FromRoute] Guid id)
    {
        await useCase.Execute(id);

        return NoContent();
    }
}
