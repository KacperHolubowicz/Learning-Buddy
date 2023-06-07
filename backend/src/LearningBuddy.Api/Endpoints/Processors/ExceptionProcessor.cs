using FluentValidation.Results;
using System.Net;

namespace LearningBuddy.Api.Endpoints.Processors
{
    public class ExceptionProcessor : IGlobalPostProcessor
    {
        public Task PostProcessAsync(object req, object? res, HttpContext ctx, IReadOnlyCollection<ValidationFailure> failures, CancellationToken ct)
        {
            ctx.Response.Headers.Add("Content-Security-Policy", "\"default-src 'self'; img-src data: 'self'; script-src 'self'; style-src 'self'; font-src 'self'; frame-src 'self'; frame-ancestors 'none'; form-action 'self';");
            ctx.Response.Headers.Add("X-Content-Type-Options", "nosniff");
            ctx.Response.Headers.Add("Strict-Transport-Security", "max-age=31536000; includeSubDomains");
            if (failures.Count > 0)
            {
                ctx.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            return Task.CompletedTask;
        }
    }
}
