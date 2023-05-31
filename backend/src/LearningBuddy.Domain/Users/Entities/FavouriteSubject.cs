using LearningBuddy.Domain.Common;

namespace LearningBuddy.Domain.Users.Entities
{
    public class FavouriteSubject : BaseEntity
    {
        public User User { get; set; }
        public Subject Subject { get; set; }
    }
}
