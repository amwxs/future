namespace Zoo.Domain.Core.Result;

public class ValidationError
{
    public string Filed { get; }
    public string Message { get; }

    public ValidationError(string filed, string message)
    {
        Filed = filed;
        Message = message;
    }
}