namespace FlowFi.Exception.ExceptionsBase;

public abstract class FlowFiException : SystemException
{
    protected FlowFiException(string message) : base(message)
    {
        
    }

    public abstract int StatusCode { get; }
    public abstract List<string> GetErrors();
}
