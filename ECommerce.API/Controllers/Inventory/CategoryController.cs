using ECommerce.Infrastrucure.Services;

namespace ECommerce.API.Controllers.Inventory
{
    [ApiController]
    public class CategoryController : ControllerBase
    {
        #region Contractor (s)
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        #endregion

        #region Display List of Category
        [HttpGet(Router.Category.ListCategory)]
        public async Task<IActionResult> GetListCategoryAsync()
        {
            var result = await _categoryService.GetAllCategoriesAsync();
            return StatusCode(result.StatusCode, result);
        }
        #endregion

        #region Display One Category By Using Id
        [HttpGet(Router.Category.GetById)]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var result = await _categoryService.GetCategoryByIdAsync(id);
            return StatusCode(result.StatusCode, result);
        }
        #endregion

        [HttpPost(Router.Category.Create)]
        public async Task<IActionResult> AddCategory([FromBody] Category category)
        {
            var result = await _categoryService.AddCategoryAsync(category);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut(Router.Category.Edit)]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] Category category)
        {
            var result = await _categoryService.UpdateCategoryAsync(id, category);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete(Router.Category.Delete)]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var result = await _categoryService.DeleteCategoryAsync(id);
            return StatusCode(result.StatusCode, result);
        }


    }
}
