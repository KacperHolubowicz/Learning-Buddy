namespace LearningBuddy.Application.Users.Commands.LoginUser
{
    public class TokenResponseDTO
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public string UserUsername { get; set; }
    }
}
