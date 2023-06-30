namespace ECommerce.Core.Responses;

public class BaseGenericResult<T> : BaseQueryResult
{
    public BaseGenericResult()
    {
    }

    public BaseGenericResult(int StatusCode, string Message = null)
    {
        this.StatusCode = StatusCode;
        this.Message = Message ?? GetDefaultMessageForStatusCode(StatusCode);
    }
    public BaseGenericResult(bool Status, int StatusCode, string Message, List<ValidationError> ValidationErrors = null) : base(Status, StatusCode, Message, ValidationErrors)
    {
        this.Status = Status;
        this.StatusCode = StatusCode;
        this.Message = Message ?? GetDefaultMessageForStatusCode(StatusCode);
        this.ValidationErrors = ValidationErrors;
    }
    public BaseGenericResult(bool Status, int StatusCode, string Message, T data, List<ValidationError> ValidationErrors = null) : this(Status, StatusCode, Message, ValidationErrors)
    {
        this.Data = data;
    }
    public T Data { get; set; }

    public object Meta { get; set; }
}
