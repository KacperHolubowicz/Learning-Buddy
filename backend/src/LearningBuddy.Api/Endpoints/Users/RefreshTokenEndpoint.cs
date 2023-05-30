using LearningBuddy.Application.Users.Commands.LoginUser;
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
            await SendAsync(await Mediator.Send(req, ct));
        }
    }
}
