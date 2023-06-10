using LearningBuddy.Application.Users.Commands.LoginUser;

namespace LearningBuddy.Api.Endpoints.Users
{
    public class LoginEndpoint : BaseEndpoint<LoginUserCommand, TokenResponseDTO>
    {
        public override void Configure()
        {
            AllowAnonymous();
            Post(Url + "login");
        }

        public override async Task HandleAsync(LoginUserCommand req, CancellationToken ct)
        {
            TokenResponseDTO res = await Mediator.Send(req, ct);
            AddRefreshTokenCookie(HttpContext, res.RefreshToken, res.ExpirationDate);
            await SendAsync(res);
        }

        public void AddRefreshTokenCookie(HttpContext context, string refreshToken, DateTimeOffset expireDate)
        {
            HttpContext.Response.Cookies.Append("RefreshToken", refreshToken, new CookieOptions()
            {
                Expires = expireDate,
                HttpOnly = true,
                SameSite = SameSiteMode.Lax,
                Secure = true,
                Domain = "localhost"
            });
        }
    }
}
