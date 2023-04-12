using ECommerce.Core.Responses;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ErrorController : BaseAPIController
    {
        [HttpGet]
        public IActionResult Error(int Code)
        {
            return new ObjectResult(new BaseGenericResult<object>(Code));
        }
    }
}
