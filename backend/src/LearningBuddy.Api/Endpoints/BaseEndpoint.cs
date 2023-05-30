using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace LearningBuddy.Api.Endpoints
{
    public abstract class BaseEndpoint<Request, Response> : Endpoint<Request, Response>
        where Request : notnull, new()
    {
        private ISender mediator = null!;
        protected ISender Mediator => mediator ??=
            HttpContext.RequestServices.GetRequiredService<ISender>();
        protected readonly string Url = "/api/v1/";
        protected int GetUserFromAuth()
        {
            var id = HttpContext.User.Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)
                ?.Value;
            if(id == null || id.IsNullOrEmpty())
            {
                return 0;
            }
            return int.Parse(id);
        }
    }

    public abstract class BaseEndpoint<Request> : Endpoint<Request, object>
        where Request : notnull, new()
    {
        private ISender mediator = null!;
        protected ISender Mediator => mediator ??=
            HttpContext.RequestServices.GetRequiredService<ISender>();
        protected readonly string Url = "/api/v1/";

        protected int GetUserFromAuth()
        {
            var id = HttpContext.User.Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)
                ?.Value;
            if (id == null || id.IsNullOrEmpty())
            {
                return 0;
            }
            return int.Parse(id);
        }
    }
}
