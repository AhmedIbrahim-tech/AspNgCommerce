using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.Responses;

public class BaseQueryResult
{
    public BaseQueryResult()
    {

    }
    public BaseQueryResult(bool Status, int StatusCode, string Message, List<ValidationError> ValidationErrors = null)
    {
        this.Status = Status;
        this.StatusCode = StatusCode;
        this.Message = Message;
        this.ValidationErrors = ValidationErrors;
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

