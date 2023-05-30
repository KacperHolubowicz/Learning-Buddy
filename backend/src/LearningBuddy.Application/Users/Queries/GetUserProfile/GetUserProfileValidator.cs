using FluentValidation;

namespace LearningBuddy.Application.Users.Queries.GetUserProfile
{
    public class GetUserProfileValidator : AbstractValidator<GetUserProfileQuery>
    {
        public GetUserProfileValidator()
        {
            RuleFor(x => x.UserID)
                .NotEmpty().WithMessage("Provided user ID cannot be empty");
        }
    }
}
