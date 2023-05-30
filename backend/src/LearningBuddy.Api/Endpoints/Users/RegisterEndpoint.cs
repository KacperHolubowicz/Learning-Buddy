using LearningBuddy.Application.Users.Commands.RegisterUser;

namespace LearningBuddy.Api.Endpoints.Users
{
    public class RegisterEndpoint : BaseEndpoint<RegisterUserCommand>
    {
        public override void Configure()
        {
            AllowAnonymous();
            Post(Url + "register");
        }

        public override async Task HandleAsync(RegisterUserCommand req, CancellationToken ct)
        {
            await SendAsync(await Mediator.Send(req, ct));
        }
    }
}
