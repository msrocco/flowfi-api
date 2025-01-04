using FlowFi.Application.UseCases.Categories.Delete;
using FlowFi.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace FlowFi.Api.Controllers;

[Route("categories")]
[ApiController]
public class CategoriesController : ControllerBase
{
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
