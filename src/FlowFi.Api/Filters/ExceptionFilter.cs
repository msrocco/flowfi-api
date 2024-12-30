using FlowFi.Communication.Responses;
using FlowFi.Exception.ExceptionsBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FlowFi.Api.Filters;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is FlowFiException)
        {
            HandleProjectException(context);
        }
        else
        {
            ThrowUnknownError(context);
        }
    }

    private void HandleProjectException(ExceptionContext context)
    {
        var flowFiException = (FlowFiException)context.Exception;
        var errorResponse = new ResponseErrorJson(flowFiException.StatusCode, flowFiException.GetErrors());

        context.HttpContext.Response.StatusCode = flowFiException.StatusCode;
        context.Result = new ObjectResult(errorResponse);
    }    
    
    private void ThrowUnknownError(ExceptionContext context)
    {
        var errorResponse = new ResponseErrorJson(StatusCodes.Status500InternalServerError, "Unknown error");

        context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Result = new ObjectResult(errorResponse);
    }
}
