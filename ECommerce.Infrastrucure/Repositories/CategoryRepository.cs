using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Infrastrucure.Repositories;

public interface ICategoryRepository : IGenericRepository<Category>
{
}

public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
{
    public CategoryRepository(ApplicationDBContext context) : base(context)
    {
    }
}
