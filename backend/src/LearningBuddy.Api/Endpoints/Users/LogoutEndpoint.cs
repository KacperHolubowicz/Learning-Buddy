using LearningBuddy.Application.Users.Commands.LogoutUser;

namespace LearningBuddy.Api.Endpoints.Users
{
    public class LogoutEndpoint : BaseEndpoint<LogoutUserCommand, bool>
    {
        public override void Configure()
        {
            Post(Url + "logout");
        }

        public override async Task HandleAsync(LogoutUserCommand req, CancellationToken ct)
        {
            req.TokenValue = HttpContext.Request.Cookies["RefreshToken"];
            await SendAsync(await Mediator.Send(req, ct));
            HttpContext.Response.Cookies.Delete("RefreshToken");
        }
    }
}
