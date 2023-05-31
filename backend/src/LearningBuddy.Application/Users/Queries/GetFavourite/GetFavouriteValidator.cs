using FluentValidation;

namespace LearningBuddy.Application.Users.Queries.GetFavouriteSubjects
{
    public class GetFavouriteValidator : AbstractValidator<GetFavouriteQuery>
    {
        public GetFavouriteValidator()
        {
            RuleFor(x => x.UserID)
                .NotEmpty().WithMessage("User id must be provided");
        }
    }
}
