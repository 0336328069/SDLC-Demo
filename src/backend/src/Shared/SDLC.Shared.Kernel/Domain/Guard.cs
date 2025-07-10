namespace SDLC.Shared.Kernel.Domain;

public static class Guard
{
    public static class Against
    {
        public static string NullOrEmpty(string value, string parameterName)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException($"{parameterName} cannot be null or empty.", parameterName);
            
            return value;
        }

        public static T Null<T>(T value, string parameterName) where T : class
        {
            return value ?? throw new ArgumentNullException(parameterName);
        }

        public static Guid Empty(Guid value, string parameterName)
        {
            if (value == Guid.Empty)
                throw new ArgumentException($"{parameterName} cannot be empty.", parameterName);
            
            return value;
        }
    }
} 