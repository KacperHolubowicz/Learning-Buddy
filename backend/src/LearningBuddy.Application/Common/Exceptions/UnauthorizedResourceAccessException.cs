namespace LearningBuddy.Application.Common.Exceptions
{
    public class UnauthorizedResourceAccessException : Exception
    {
        public UnauthorizedResourceAccessException() : base("Access to the resource cannot be granted due to lack of permissions")
        {
        }

        public UnauthorizedResourceAccessException(string message) : base(message)
        {
        }

        public UnauthorizedResourceAccessException(string resourceName, long resourceId) :
            base($"Resource {resourceName} with id {resourceId} cannot be accessed")
        {
        }
    }
}
