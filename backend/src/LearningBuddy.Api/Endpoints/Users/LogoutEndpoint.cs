using LearningBuddy.Application.Users.Commands.LogoutUser;

namespace LearningBuddy.Api.Endpoints.Users
{
    public class LogoutEndpoint : BaseEndpoint<LogoutUserCommand>
    {
        public override void Configure()
        {
            Post(Url + "logout");
        }

        public override async Task HandleAsync(LogoutUserCommand req, CancellationToken ct)
        {
            await SendAsync(await Mediator.Send(req, ct));
        }
    }
}
