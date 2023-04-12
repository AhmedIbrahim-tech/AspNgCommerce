using ECommerce.Core.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuggyController : BaseAPIController
    {
        private readonly ApplicationDBContext context;

        public BuggyController(ApplicationDBContext context)
        {
            this.context = context;
        }
        [HttpGet("NotFound")]
        public IActionResult GetNotFoundRequest()
        {
            var thing = context.Products.Find(50);
            if(thing == null) { return NotFound(new BaseGenericResult<object>(404)); }
            return Ok();
        }

        [HttpGet("ServerError")]
        public IActionResult GetServerErrorRequest()
        {
            var thing = context.Products.Find(50);
            var thingtoReturn = thing.ToString();
            return Ok();
        }

        [HttpGet("BadRequest")]
        public IActionResult GetBadRequest()
        {
            return BadRequest();
        }

        [HttpGet("BadRequest/{id}")]
        public IActionResult GetBadRequest(int id)
        {
            return Ok();
        }
    }
}
