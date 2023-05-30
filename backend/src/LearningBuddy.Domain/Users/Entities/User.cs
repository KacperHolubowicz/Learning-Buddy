namespace LearningBuddy.Domain.Users.Entities
{
    public class User : BaseEntity
    {
        public string Username { get; set; } = "";
        public string? Description { get; set; }
        public string Login { get; set; } = "";
        public string Email { get; set; } = ""; 
        public byte[] Password { get; set; }
        public byte[] Salt { get; set; }
        public ICollection<LearningSource> Sources { get; set; }
        public ICollection<Subject> Subjects { get; set; }
        public ICollection<Quiz> Quizzes { get; set; }
        public ICollection<Attempt> Attempts { get; set; }
        public ICollection<SubjectTask> Tasks { get; set; }
        public ICollection<RefreshToken> RefreshTokens { get; set; }
    }
}
