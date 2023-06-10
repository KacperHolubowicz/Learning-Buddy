using LearningBuddy.Application.Common.Exceptions;
using LearningBuddy.Application.Users.Commands.RefreshToken;

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
            req.RefreshToken = GetRefreshTokenFromCookie(HttpContext);
            await SendAsync(await Mediator.Send(req, ct));
        }

        private string GetRefreshTokenFromCookie(HttpContext context)
        {
            if (!context.Request.Cookies.TryGetValue("RefreshToken", out string refresh))
            {
                throw new InvalidRefreshTokenException("No refresh token was provided in cookies. Consider signing in again.");
            }

            return refresh;
        }
    }
}
