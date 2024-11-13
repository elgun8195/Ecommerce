using AzureProject.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureProject.Business.Abstract
{
    public interface ICategoryService
    {
        List<Category> GetList();
        Category GetByIdCategory(int id); // Dal dan gelen T nesnesini karsilar
        void CategoryAdd(Category category);
        void CategoryDelete(Category category);
        void CategoryUpdate(Category category);
    }
}
