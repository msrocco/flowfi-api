using System.Net;

namespace FlowFi.Exception.ExceptionsBase;

public class NotFoundException : FlowFiException
{
    public NotFoundException(string message) : base(message)
    {
    }

    public override int StatusCode => (int)HttpStatusCode.NotFound;

    public override List<string> GetErrors()
    {
        return [Message];
    }
}
