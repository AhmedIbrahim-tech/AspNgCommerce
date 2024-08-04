using AutoMapper;
using ECommerce.Core.CustomEntities;
using ECommerce.Core.Responses;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Infrastrucure.Services;

public interface ICategoryService
{
    Task<BaseGenericResult<IReadOnlyList<Category>>> GetAllCategoryAsync();
    Task<BaseGenericResult<Category>> GetCategoryByIdAsync(int id);

}
public class CategoryService : BaseGenericResultHandler,ICategoryService
{
    #region Contractor (s) 
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this._unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    #endregion


    public async Task<BaseGenericResult<IReadOnlyList<Category>>> GetAllCategoryAsync()
    {
        try
        {
            var result = await _unitOfWork.GenericCategoryRepository.ListAllAsync();
            return Success(result);
        }
        catch (Exception ex)
        {
            return InternalServer<IReadOnlyList<Category>>(ex.Message);
        }
    }

    public async Task<BaseGenericResult<Category>> GetCategoryByIdAsync(int id)
    {
        try
        {
            var result = await _unitOfWork.GenericCategoryRepository.GetByIdAsync(id);
            return Success(result);
        }
        catch (Exception ex)
        {
            return InternalServer<Category>(ex.Message);
        }
    }
}
