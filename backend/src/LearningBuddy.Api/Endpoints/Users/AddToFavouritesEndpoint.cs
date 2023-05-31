using LearningBuddy.Application.Users.Commands.Favourites;

namespace LearningBuddy.Api.Endpoints.Users
{
    public class AddToFavouritesEndpoint : BaseEndpoint<AddToFavouritesCommand>
    {
        public override void Configure()
        {
            Post(Url + "user/favourites");
        }

        public override async Task HandleAsync(AddToFavouritesCommand req, CancellationToken ct)
        {
            int userId = GetUserFromAuth();
            if (userId == 0)
            {
                await SendUnauthorizedAsync(ct);
            }
            req.UserID = userId;
            await SendAsync(await Mediator.Send(req, ct));
        }
    }
}
