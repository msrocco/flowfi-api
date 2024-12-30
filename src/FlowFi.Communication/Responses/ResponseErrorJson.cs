namespace FlowFi.Communication.Responses;

public class ResponseErrorJson
{
    public int StatusCode { get; set; }
    public List<string> ErrorMessages { get; set; }

    public ResponseErrorJson(int statusCode, string errorMessage)
    {
        StatusCode = statusCode;
        ErrorMessages = [errorMessage];
    }

    public ResponseErrorJson(int statusCode, List<string> errorMessages)
    {
        StatusCode = statusCode;
        ErrorMessages = errorMessages;
    }
}
