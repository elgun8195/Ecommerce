using AzureProject.Business.Abstract;
using AzureProject.DataAccess.Abstract;
using AzureProject.DataAccess.UnitOfWork.Interface;
using AzureProject.Entity.Concrete;
using System;
using System.Collections.Generic;

namespace AzureProject.Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private readonly ICategoryDal _categoryDal;
        private readonly IUnitOfWork _unitOfWork;

        // ICategoryDal və IUnitOfWork inject edirik
        public CategoryManager(ICategoryDal categoryDal, IUnitOfWork unitOfWork)
        {
            _categoryDal = categoryDal;
            _unitOfWork = unitOfWork;
        }

        public void CategoryAdd(Category category)
        {
            if (category==null)
            {
                throw new ArgumentNullException(nameof(category), "Category cannot be null.");
            }
            category.CreatedTime = DateTime.Now;
            _categoryDal.Insert(category);
            _unitOfWork.SaveAsync(); // Dəyişiklikləri yaddaşa yazır
        }

        public void CategoryDelete(Category category)
        {
            if (category == null)
            {
                throw new ArgumentNullException(nameof(category), "Category cannot be null.");
            }
            category.DeletedTime= DateTime.Now;
            category.IsDeleted = true; // Silinmə statusunu dəyişdir
            _categoryDal.Update(category);
            _unitOfWork.SaveAsync(); // Dəyişiklikləri yaddaşa yazır
        }

        public void CategoryUpdate(Category category)
        {
            if (category == null)
            {
                throw new ArgumentNullException(nameof(category), "Category cannot be null.");
            }
            category.UpdatedTime = DateTime.Now;
            _categoryDal.Update(category);
            _unitOfWork.SaveAsync(); // Dəyişiklikləri yaddaşa yazır
        }

        public Category GetByIdCategory(int id)
        {
            return _categoryDal.Get(x => x.Id == id); // DAL dan gelen ilgili alani karsilar
        }

        public List<Category> GetList()
        {
            return _categoryDal.List(c => c.IsDeleted == false);
        }
    }
}
