namespace CashFlow.Communication.Responses;

public class ResponseErrorJson
{
    public ResponseErrorJson(string message)
    {
        ErrorMessages = [message];
    }

    public ResponseErrorJson(List<string> messages)
    {
        ErrorMessages = messages;
    }
    public List<string> ErrorMessages { get; set; }
}
