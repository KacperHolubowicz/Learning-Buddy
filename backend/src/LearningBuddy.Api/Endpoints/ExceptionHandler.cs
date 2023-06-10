using LearningBuddy.Application.Common.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace FastEndpoints;

internal class ExceptionHandler { }

/// <summary>
/// extensions for global exception handling
/// </summary>
public static class ExceptionHandlerExtensions
{
    public static IApplicationBuilder UseModifiedExceptionHandler(this IApplicationBuilder app, ILogger? logger = null, bool logStructuredException = false)
    {
        app.UseExceptionHandler(errApp =>
        {
            errApp.Run(async ctx =>
            {
                var exHandlerFeature = ctx.Features.Get<IExceptionHandlerFeature>();
                if (exHandlerFeature is not null)
                {
                    logger ??= ctx.Resolve<ILogger<ExceptionHandler>>();
                    var http = exHandlerFeature.Endpoint?.DisplayName?.Split(" => ")[0];
                    var type = exHandlerFeature.Error.GetType().Name;
                    var error = exHandlerFeature.Error.Message;
                    string status = "Internal Server Error!";
                    int statusCode = (int)HttpStatusCode.InternalServerError;
                    Exception thrownException = exHandlerFeature.Error;
                    var msg =
$@"================================= 
{http} 
TYPE: {type} 
REASON: {error} 
--------------------------------- 
{thrownException.StackTrace}";

                    if (logStructuredException)
                        logger.LogError("{@http}{@type}{@reason}{@exception}", http, type, error, exHandlerFeature.Error);
                    else
                        logger.LogError(msg);

                    switch(thrownException)
                    {
                        case ResourceNotFoundException rnfe:
                            status = "Resource not found";
                            statusCode = (int)HttpStatusCode.NotFound;
                            break;
                        case InvalidRefreshTokenException irte:
                            status = "Expired or invalid refresh token";
                            statusCode = (int)HttpStatusCode.Unauthorized;
                            break;
                        case PropertyNotUniqueException pnue:
                            status = "Not unique property";
                            statusCode = (int)HttpStatusCode.BadRequest;
                            break;
                        case UnauthorizedResourceAccessException urae:
                            status = "Unauthorized access to resource";
                            statusCode = (int)HttpStatusCode.Forbidden;
                            break;
                    }

                    ctx.Response.StatusCode = statusCode;
                    ctx.Response.ContentType = "application/problem+json";
                    await ctx.Response.WriteAsJsonAsync(new InternalErrorResponse
                    {
                        Status = status,
                        Code = ctx.Response.StatusCode,
                        Reason = error,
                        Note = "See application log for stack trace."
                    });
                }
            });
        });

        return app;
    }
}