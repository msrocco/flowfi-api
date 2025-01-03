using FlowFi.Application.UseCases.BankAccounts.Create;
using FlowFi.Application.UseCases.Transactions.Create;
using FlowFi.Communication.Requests;
using FlowFi.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace FlowFi.Api.Controllers;

[Route("transactions")]
[ApiController]
public class TransactionsController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(
        [FromServices] ICreateTransactionUseCase useCase,
        [FromBody] RequestTransactionJson request)
    {
        await useCase.Execute(request);

        return StatusCode(StatusCodes.Status201Created);
    }
}
