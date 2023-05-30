namespace LearningBuddy.Application.Common.Exceptions
{
    public class ResourceNotFoundException : Exception
    {
        public ResourceNotFoundException() : base("Resource you were looking for does not exist")
        {
        }

        public ResourceNotFoundException(string message) : base(message)
        {
        }

        public ResourceNotFoundException(string entityName, long id)
            : base($"Entity {entityName} with id {id} was not found")
        {
        }
    }
}
