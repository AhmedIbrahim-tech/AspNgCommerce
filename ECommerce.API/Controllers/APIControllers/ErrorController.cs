namespace ECommerce.API.Controllers.APIControllers
{
    [Route("errors/{code}")]
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
