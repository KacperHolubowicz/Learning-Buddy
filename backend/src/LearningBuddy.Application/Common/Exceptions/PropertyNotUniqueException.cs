namespace LearningBuddy.Application.Common.Exceptions
{
    public class PropertyNotUniqueException : Exception
    {
        public PropertyNotUniqueException() : base("One or more of your properties are not unique.")
        {
        }

        public PropertyNotUniqueException(string property) : base($"Property '{property}' is not unique.")
        {
        }

        public PropertyNotUniqueException(string property, string value)
            : base($"Property '{property}' with value '{value}' already exists.")
        {
        }

        public PropertyNotUniqueException(IDictionary<string, string> properties)
            : base(CreateMessage(properties))
        {
        }

        private static string CreateMessage(IDictionary<string, string> properties)
        {
            string message = "Some of your properties are not unique:\n";
            foreach (string key in properties.Keys)
            {
                message += $"Property: {key}; Value: {properties[key]}";
            }
            return message;
        }
    }
}
