namespace ECommerce.Core.Responses;

public class BaseGenericResultHandler
{
    public BaseGenericResult<T> Delete<T>()
    {
        return new BaseGenericResult<T>()
        {
            StatusCode = (int)HttpStatusCode.OK,
            Status = true,
            Message = "Deleted Successfully"
        };
    }

    public BaseGenericResult<T> Success<T>(T entity, object meta = null)
    {
        return new BaseGenericResult<T>()
        {
            StatusCode = (int)HttpStatusCode.OK,
            Status = true,
            Message = "Data Loading Successfully",
            Data = entity,
            Meta = meta
        };
    }

    public BaseGenericResult<T> Unauthorized<T>()
    {
        return new BaseGenericResult<T>()
        {
            StatusCode = (int)HttpStatusCode.Unauthorized,
            Status = true,
            Message = "Unauthorized",
        };
    }
    public BaseGenericResult<T> BadRequest<T>(string Message = null)
    {
        return new BaseGenericResult<T>()
        {
            StatusCode = (int)HttpStatusCode.BadRequest,
            Status = false,
            Message = Message == null ? "Bad Request" : Message
        };
    }

    public BaseGenericResult<T> UnProcessableEntity<T>(string Message = null)
    {
        return new BaseGenericResult<T>()
        {
            StatusCode = (int)HttpStatusCode.UnprocessableEntity,
            Status = false,
            Message = Message == null ? "Un-processable Entity" : Message
        };
    }

    public BaseGenericResult<T> AlreadyExit<T>(string Message = null)
    {
        return new BaseGenericResult<T>()
        {
            StatusCode = (int)HttpStatusCode.OK,
            Status = false,
            Message = Message == null ? "Object Already Exist" : Message
        };
    }

    public BaseGenericResult<T> NotFound<T>(string message = null)
    {
        return new BaseGenericResult<T>()
        {
            StatusCode = (int)HttpStatusCode.NotFound,
            Status = false,
            Message = message == null ? "Object Not-Found" : message
        };
    }

    public BaseGenericResult<T> InternalServer<T>(string message = null)
    {
        return new BaseGenericResult<T>()
        {
            StatusCode = (int)HttpStatusCode.InternalServerError,
            Status = false,
            Message = message == null ? "Internal Server Error" : message
        };
    }

    public BaseGenericResult<T> Created<T>(T entity, object Meta = null)
    {
        return new BaseGenericResult<T>()
        {
            Data = entity,
            StatusCode = (int)HttpStatusCode.Created,
            Status = true,
            Message = "Created Successfully",
            Meta = Meta
        };
    }

    public BaseGenericResult<T> Updated<T>(T entity, object Meta = null)
    {
        return new BaseGenericResult<T>()
        {
            Data = entity,
            StatusCode = (int)HttpStatusCode.OK,
            Status = true,
            Message = "Updated Successfully",
            Meta = Meta
        };
    }
}