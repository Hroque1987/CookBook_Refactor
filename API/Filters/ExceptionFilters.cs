using System;
using System.Net;
using Communication.Responses;
using Exceptions;
using Exceptions.Exceptionbase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace API.Filters;

public class ExceptionFilters : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if(context.Exception is MyRecipyBookException)
        {
            HandleProjectException(context);
        }else
        {
            ThrowUnknownException(context);
        }
    }

    private void HandleProjectException(ExceptionContext exceptionContext)
    {
                
        if (exceptionContext?.Exception is ErrorOnValidationException exception )
        {    
            var problem = new ValidationProblemDetails()
            {
                Status = StatusCodes.Status400BadRequest,
                Title = ResourceMessagesException.VALIDATION_FAILED,
                Detail = ResourceMessagesException.SEE_ERRORS
            };

             problem.Errors.Add("Validation", [.. exception.ErrorMessages]);

            exceptionContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            exceptionContext.Result = new BadRequestObjectResult(problem);
        } 
        else
        {
            ThrowUnknownException(exceptionContext);
        }
    }

    private void ThrowUnknownException(ExceptionContext? exceptionContext)
    {
        if(exceptionContext != null)
        {
            exceptionContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            exceptionContext.Result =  exceptionContext.Result = new ObjectResult(new ResponseErrorJson(ResourceMessagesException.UNKNOWN_ERROR));
        }
        
    }
}
