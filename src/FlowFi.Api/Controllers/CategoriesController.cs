using FlowFi.Application.UseCases.Categories.Create;
using FlowFi.Application.UseCases.Categories.Delete;
using FlowFi.Application.UseCases.Categories.GetAll;
using FlowFi.Communication.Requests;
using FlowFi.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace FlowFi.Api.Controllers;

[Route("categories")]
[ApiController]
public class CategoriesController : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(ResponseCategoriesJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetAllCategories(
    [FromServices] IGetAllCategoriesUseCase useCase)
    {
        var response = await useCase.Execute();

        if (response.Categories.Count != 0)
            return Ok(response);

        return NoContent();
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(
     [FromServices] ICreateCategoryUseCase useCase,
     [FromBody] RequestCategoryJson request)
    {
        await useCase.Execute(request);

        return StatusCode(StatusCodes.Status201Created);
    }

    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(
    [FromServices] IDeleteCategoryUseCase useCase,
    [FromRoute] Guid id)
    {
        await useCase.Execute(id);

        return NoContent();
    }
}
