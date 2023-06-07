using LearningBuddy.Application.Common.Exceptions;
using LearningBuddy.Application.Users.Commands.RefreshToken;
using Microsoft.Extensions.Primitives;

namespace LearningBuddy.Api.Endpoints.Users
{
    public class RefreshTokenEndpoint : BaseEndpoint<RefreshTokenCommand, RefreshTokenResponseDTO>
    {
        public override void Configure()
        {
            Post(Url + "refresh-token");
            AllowAnonymous();
        }

        public override async Task HandleAsync(RefreshTokenCommand req, CancellationToken ct)
        {
            string refresh;
            if(!HttpContext.Request.Cookies.TryGetValue("RefreshToken", out refresh))
            {
                throw new InvalidRefreshTokenException("No refresh token was provided in headers. Consider signing in again.");
            }
            req.RefreshToken = refresh;
            await SendAsync(await Mediator.Send(req, ct));
        }
    }
}
