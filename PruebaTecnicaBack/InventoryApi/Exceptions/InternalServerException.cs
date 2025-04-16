namespace AdministracionApi.Exceptions;

public class InternalServerException : Exception
{
    protected InternalServerException(string message) : base(message){}

    protected InternalServerException(string message, string details) : base(message)
    {
        Details = details;
    }
    public string? Details { get; }
}