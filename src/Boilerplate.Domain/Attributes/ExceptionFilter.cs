namespace Boilerplate.Domain
{
    #region << Using >>

    using System;
    using System.Net;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;

    #endregion

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var statusCode = HttpStatusCode.InternalServerError;

            if (context.Exception is EntityNotFoundException)
                statusCode = HttpStatusCode.NotFound;

            context.HttpContext.Response.ContentType = Constants.HttpContentType.ContentType;
            ;
            context.HttpContext.Response.StatusCode = (int)statusCode;

            context.Result = new JsonResult(new
                                            {
                                                    error = new[] { context.Exception.Message },
                                                    StackTrace = context.Exception.StackTrace
                                            });
        }
    }
}