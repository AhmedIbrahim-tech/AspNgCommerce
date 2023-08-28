namespace ECommerce.Core.Responses;

public class BaseQueryResult
{
    public BaseQueryResult()
    {

    }
    public BaseQueryResult(int StatusCode, string Message = null)
    {
        this.StatusCode = StatusCode;
        this.Message = Message ?? GetDefaultMessageForStatusCode(StatusCode);

    }
    public BaseQueryResult(bool Status, int StatusCode, string Message, List<ValidationError> ValidationErrors = null)
    {
        this.Status = Status;
        this.StatusCode = StatusCode;
        this.Message = Message ?? GetDefaultMessageForStatusCode(StatusCode);
        this.ValidationErrors = ValidationErrors;
    }

    public string GetDefaultMessageForStatusCode(int statusCode)
    {
        return statusCode switch
        {
            400 => "A bad Request, You have Made",
            401 => "Authorized you are not",
            404 => "Response found it is not",
            500 => "Server error occurred",
            600 => "Email Or Password is incorrect",
            _ => null
        };
    }

    public bool Status { get; set; }
    public string Message { get; set; }
    public int StatusCode { get; set; }
    //    get
    //    {
    //        return this.StatusCode;
    //    } 
    //    set
    //    {
    //        this.StatusCode = this.Status ? (int)HttpStatusCode.OK : (int)HttpStatusCode.BadRequest;
    //    }
    //}
    public List<ValidationError> ValidationErrors { get; set; }

}
public class ValidationError
{
    public string Code { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}

