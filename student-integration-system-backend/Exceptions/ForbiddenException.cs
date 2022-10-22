namespace student_integration_system_backend.Exceptions;

public class ForbiddenException : AppException
{
    public ForbiddenException(string message) : base(message)
    {
    }
}