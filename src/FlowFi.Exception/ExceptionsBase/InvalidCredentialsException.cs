using System.Net;

namespace FlowFi.Exception.ExceptionsBase;

public class InvalidCredentialsException : FlowFiException
{
    public InvalidCredentialsException(string message) : base(message)
    {
    }
    public override int StatusCode => (int)HttpStatusCode.Unauthorized;
    public override List<string> GetErrors()
    {
        return [Message];
    }
}
