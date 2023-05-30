namespace LearningBuddy.Domain.Users.Entities
{
    public class RefreshToken : BaseEntity
    {
        public string Value { get; set; }
        public DateTimeOffset ExpirationTime { get; set; }
        public User User { get; set; }
    }
}
