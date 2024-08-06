using AutoMapper;
using ECommerce.Core.Responses;
using System.Net;

namespace ECommerce.Infrastrucure.Services;

public interface ICategoryService
{
    Task<BaseGenericResult<Category>> GetCategoryByIdAsync(int id);
    Task<BaseGenericResult<IEnumerable<Category>>> GetAllCategoriesAsync();
    Task<BaseGenericResult<Category>> AddCategoryAsync(Category category);
    Task<BaseGenericResult<Category>> UpdateCategoryAsync(int id, Category category);
    Task<BaseGenericResult<int>> DeleteCategoryAsync(int id);   

}
public class CategoryService : ICategoryService
{
    private readonly IUnitOfWork _unitOfWork;

    public CategoryService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<BaseGenericResult<Category>> GetCategoryByIdAsync(int id)
    {
        try
        {
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(id);
            if (category == null)
                return new(false, (int)HttpStatusCode.NotFound, "Category not found.");

            return new(true, (int)HttpStatusCode.OK, "Category loaded successfully.", category);
        }
        catch (Exception ex)
        {
            return new(false, (int)HttpStatusCode.InternalServerError, "Loading category failed → " + ex.Message);
        }
    }

    public async Task<BaseGenericResult<IEnumerable<Category>>> GetAllCategoriesAsync()
    {
        try
        {
            var categories = await _unitOfWork.CategoryRepository.ListAllAsync();
            return new BaseGenericResult<IEnumerable<Category>>(true, (int)HttpStatusCode.OK, "Categories loaded successfully.", categories);
        }
        catch (Exception ex)
        {
            return new BaseGenericResult<IEnumerable<Category>>(false, (int)HttpStatusCode.InternalServerError, "Loading categories failed → " + ex.Message);
        }
    }

    public async Task<BaseGenericResult<Category>> AddCategoryAsync(Category category)
    {
        try
        {
            await _unitOfWork.CategoryRepository.AddAsync(category);
            await _unitOfWork.SaveChangesAsync();

            return new(true, (int)HttpStatusCode.Created, "Category added successfully.", category);
        }
        catch (Exception ex)
        {
            return new(false, (int)HttpStatusCode.InternalServerError, "Adding category failed → " + ex.Message);
        }
    }

    public async Task<BaseGenericResult<Category>> UpdateCategoryAsync(int id, Category category)
    {
        try
        {
            var existingCategory = await _unitOfWork.CategoryRepository.GetByIdAsync(id);
            if (existingCategory == null)
                return new(false, (int)HttpStatusCode.NotFound, "Category not found.");

            existingCategory.Name = category.Name;
            existingCategory.Description = category.Description;

            _unitOfWork.CategoryRepository.EditAsync(existingCategory);
            await _unitOfWork.SaveChangesAsync();

            return new(true, (int)HttpStatusCode.OK, "Category updated successfully.", existingCategory);
        }
        catch (Exception ex)
        {
            return new(false, (int)HttpStatusCode.InternalServerError, "Updating category failed → " + ex.Message);
        }
    }

    public async Task<BaseGenericResult<int>> DeleteCategoryAsync(int id)
    {
        try
        {
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(id);
            if (category == null)
                return new(false, (int)HttpStatusCode.NotFound, "Category not found.");

            await _unitOfWork.CategoryRepository.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();

            return new(true, (int)HttpStatusCode.OK, "Category deleted successfully.");
        }
        catch (Exception ex)
        {
            return new(false, (int)HttpStatusCode.InternalServerError, "Deleting category failed → " + ex.Message);
        }
    }
}
