namespace ECommerce.API.Controllers.APIControllers
{
    [ApiController]
    public class BuggyController : BaseAPIController
    {
        private readonly ApplicationDBContext context;

        public BuggyController(ApplicationDBContext context)
        {
            this.context = context;
        }


        [HttpGet(Router.Error.NotFound)]
        public IActionResult GetNotFoundRequest()
        {
            var thing = context.Products.Find(99);
            if (thing == null) { return NotFound(new BaseGenericResult<object>(404)); }
            return Ok();
        }

        [HttpGet(Router.Error.ServerError)]
        public IActionResult GetServerErrorRequest()
        {
            var item = context.Products.Find(99);
            var itemOperation = item.ToString();
            return Ok();
        }

        [HttpGet(Router.Error.BadRequest)]
        public IActionResult GetBadRequest()
        {
            return BadRequest(new BaseGenericResult<object>(400));
        }

        [HttpGet(Router.Error.GetBadRequestById)]
        public IActionResult GetBadRequest(int id)
        {
            return BadRequest();
        }
    }
}
