using FluentValidation;

namespace LearningBuddy.Application.Users.Commands.Favourites
{
    public class AddToFavouritesValidator : AbstractValidator<AddToFavouritesCommand>
    {
        public AddToFavouritesValidator()
        {
            RuleFor(x => x.UserID)
                .NotEmpty()
                .WithMessage("User id is required");
            RuleFor(x => x.ObjectID)
                .NotEmpty()
                .WithMessage("Added object id is required");
        }
    }
}
