using ECommerce.Core.Services;
using ECommerce.Infrastrucure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        #region Contractor (s)
        private readonly ICategoryService categoryService;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryService categoryService, IMapper mapper)
        {
            this.categoryService = categoryService;
            _mapper = mapper;
        }

        #endregion

        #region Display List of Category
        [HttpGet(Router.Category.ListCategory)]
        public async Task<IActionResult> GetListCategoryAsync()
        {
            var result = await categoryService.GetAllCategoryAsync();
            return StatusCode(result.StatusCode, result);
        }
        #endregion


        #region Display One Category By Using Id
        [HttpGet(Router.Category.GetById)]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var result = await categoryService.GetCategoryByIdAsync(id);
            return StatusCode(result.StatusCode, result);
        }
        #endregion

    }
}
