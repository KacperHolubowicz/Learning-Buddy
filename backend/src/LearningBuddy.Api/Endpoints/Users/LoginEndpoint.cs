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
            await SendAsync(await Mediator.Send(req, ct));
        }
    }
}
