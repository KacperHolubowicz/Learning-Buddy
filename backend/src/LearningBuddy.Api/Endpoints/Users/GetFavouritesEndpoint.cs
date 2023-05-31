using LearningBuddy.Application.Users.Queries.GetFavourite;
using LearningBuddy.Application.Users.Queries.GetFavouriteSubjects;

namespace LearningBuddy.Api.Endpoints.Users
{
    public class GetFavouritesEndpoint : BaseEndpoint<GetFavouriteQuery, GetFavouriteResponse>
    {
        public override void Configure()
        {
            Get(Url + "user/favourites");
            ResponseCache(60);
        }

        public override async Task HandleAsync(GetFavouriteQuery req, CancellationToken ct)
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
