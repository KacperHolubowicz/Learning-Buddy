using LearningBuddy.Domain.Common;

namespace LearningBuddy.Domain.Users.Entities
{
    public class FavouriteQuiz : BaseEntity
    {
        public User User { get; set; }
        public Quiz Quiz { get; set; }
    }
}
