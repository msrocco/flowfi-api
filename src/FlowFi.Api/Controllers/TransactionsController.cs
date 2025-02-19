﻿using FlowFi.Application.UseCases.Transactions.Create;
using FlowFi.Application.UseCases.Transactions.Delete;
using FlowFi.Application.UseCases.Transactions.GetAll;
using FlowFi.Application.UseCases.Transactions.Update;
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
        [FromServices] IGetAllTransactionsUseCase useCase,
        [FromQuery] int? month = null,
        [FromQuery] int? year = null,
        [FromQuery] Guid? bankAccountId = null,
        [FromQuery] string? type = null)
    {
        var response = await useCase.Execute(month, year, bankAccountId, type);

        if (response.Transactions.Count != 0)
            return Ok(response);

        return NoContent();
    }

    [HttpPut]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(
    [FromServices] IUpdateTransactionUseCase useCase,
    [FromRoute] Guid id,
    [FromBody] RequestTransactionJson request)
    {
        await useCase.Execute(id, request);

        return NoContent();
    }

    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(
    [FromServices] IDeleteTransactionUseCase useCase,
    [FromRoute] Guid id)
    {
        await useCase.Execute(id);

        return NoContent();
    }
}
