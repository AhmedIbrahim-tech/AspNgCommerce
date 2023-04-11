using ECommerce.Core.CustomEntities;

namespace ECommerce.Core.Responses;

public class BaseGenericResult<T> : BaseQueryResult
{
    public BaseGenericResult(bool v, int oK)
    {

    }
    public BaseGenericResult(bool Status, int StatusCode, string Message, List<ValidationError> ValidationErrors = null) : base(Status, StatusCode, Message, ValidationErrors)
    {
        this.Status = Status;
        this.StatusCode = StatusCode;
        this.Message = Message;
        this.ValidationErrors = ValidationErrors;
    }
    public BaseGenericResult(bool Status, int StatusCode, string Message, T data, List<ValidationError> ValidationErrors = null) : this(Status, StatusCode, Message, ValidationErrors)
    {
        this.Data = data;
    }
    public T Data { get; set; }

    public Metadata Meta { get; set; }
}
