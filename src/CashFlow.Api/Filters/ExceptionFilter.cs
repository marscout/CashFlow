using CashFlow.Communication.Responses;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CashFlow.Api.Filters;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if(context.Exception is CashFlowException)
        {
            HandleProjectException(context);
        }
        else
        {
            ThrowUnkownError(context);
        }
        
    }
    private void HandleProjectException(ExceptionContext context)
    {
        var cashFlowEx = context.Exception as CashFlowException;
        var errorReponse = new ResponseErrorJson(cashFlowEx!.GetErrors());

        context.HttpContext.Response.StatusCode = cashFlowEx.StatusCode;
        context.Result = new ObjectResult(errorReponse);
        //if(context.Exception is ErrorOnValidationException ErrorValidationEx)
        //{
        //    
        //    context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        //    context.Result = new BadRequestObjectResult(errorReponse);

        //}else if (context.Exception is NotFoundException exnotFoundEx)
        //{
        //    var errorReponse = new ResponseErrorJson(exnotFoundEx.Message);
        //    context.HttpContext.Response.StatusCode = StatusCodes.Status404NotFound;
        //    context.Result = new BadRequestObjectResult(errorReponse);
        //}
        //else
        //{
        //    var errorReponse = new ResponseErrorJson(context.Exception.Message);
        //    context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        //    context.Result = new BadRequestObjectResult(errorReponse);
        //}
    }

    private void ThrowUnkownError(ExceptionContext context)
    {
        context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Result = new ObjectResult(new ResponseErrorJson(ResourceErrorMessages.UNKNOWN_ERROR));
    }
}
