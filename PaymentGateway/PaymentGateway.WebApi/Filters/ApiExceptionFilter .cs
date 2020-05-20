using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using PaymentGateway.Exceptions;
using System;
using System.Net;

namespace PaymentGateway.WebApi.Filters
{
    public class ApiExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is ArgumentException)
            {
                context.ExceptionHandled = true;
                HttpResponse response = context.HttpContext.Response;
                response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            else if (context.Exception is PaymentResponseNotFoundException)
            {
                context.ExceptionHandled = true;
                HttpResponse response = context.HttpContext.Response;
                response.StatusCode = (int)HttpStatusCode.NotFound;
            }




            base.OnException(context);
        }
    }
}
