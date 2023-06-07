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
            HttpContext.Response.Cookies.Append("RefreshToken", res.RefreshToken, new CookieOptions()
            {
                Expires = res.ExpirationDate,
                HttpOnly = true,
                SameSite = SameSiteMode.Lax,
                Secure = true,
                Domain = "localhost"
            });
            await SendAsync(res);
        }
    }
}
