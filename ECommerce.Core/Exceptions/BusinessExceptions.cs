namespace ECommerce.Core.Exceptions;

public class BusinessExceptions : Exception
{
    public BusinessExceptions()
    {
    }

    public BusinessExceptions(string Message) : base(Message)
    {

    }
}
