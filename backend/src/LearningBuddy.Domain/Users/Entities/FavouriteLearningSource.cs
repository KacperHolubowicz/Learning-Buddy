namespace LearningBuddy.Domain.Users.Entities
{
    public class FavouriteLearningSource : BaseEntity
    {
        public User User { get; set; }
        public LearningSource LearningSource { get; set; }
    }
}
