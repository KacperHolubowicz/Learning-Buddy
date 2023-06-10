using LearningBuddy.Application.Users.Commands.Favourites;

namespace LearningBuddy.Api.Endpoints.Users
{
    public class AddToFavouritesEndpoint : BaseEndpoint<AddToFavouritesCommand, bool>
    {
        public override void Configure()
        {
            Post(Url + "user/favourites");
        }

        public override async Task HandleAsync(AddToFavouritesCommand req, CancellationToken ct)
        {
            int userId = GetUserFromAuth();
            req.UserID = userId;
            await SendAsync(await Mediator.Send(req, ct));
        }
    }
}
