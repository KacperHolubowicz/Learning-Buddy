using LearningBuddy.Application.Users.Queries.GetUserProfile;

namespace LearningBuddy.Api.Endpoints.Users
{
    public class GetUserProfileEndpoint : BaseEndpoint<GetUserProfileQuery, UserProfileDTO>
    {
        public override void Configure()
        {
            Get(Url + "profile");
        }

        public override async Task HandleAsync(GetUserProfileQuery req, CancellationToken ct)
        {
            int userId = GetUserFromAuth();
            if(userId == 0)
            {
                await SendUnauthorizedAsync(ct);
            }
            req.UserID = userId;
            await SendAsync(await Mediator.Send(req, ct));
        }
    }
}
