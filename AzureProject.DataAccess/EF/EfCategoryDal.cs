using AzureProject.Entity.Concrete;
using AzureProject.DataAccess.Abstract;
using AzureProject.DataAccess.DAL;
using AzureProject.DataAccess.Repositories;

namespace AzureProject.DataAccess.EF
{
    public class EfCategoryDal : GenericRepository<Category>, ICategoryDal
    {
        public EfCategoryDal(AppDbContext context) : base(context)
        {
        }
    }
}
