namespace LearningBuddy.Application.Common.Exceptions
{
    public class InvalidRefreshTokenException : Exception
    {
        public InvalidRefreshTokenException() : base("This refresh token has expired or is invalid")
        {
        }
    }
}
