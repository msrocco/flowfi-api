using FlowFi.Application.UseCases.Transactions.Create;
using FlowFi.Application.UseCases.Transactions.GetAll;
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

    [HttpGet]
    [ProducesResponseType(typeof(ResponseTransactionsJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetAllTransactions(
    [FromServices] IGetAllTransactionsUseCase useCase)
    {
        var response = await useCase.Execute();

        if (response.Transactions.Count != 0)
            return Ok(response);

        return NoContent();
    }
}
