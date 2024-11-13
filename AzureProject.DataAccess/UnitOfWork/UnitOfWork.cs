using AzureProject.DataAccess.Abstract;
using AzureProject.DataAccess.DAL;
using AzureProject.DataAccess.Repositories;
using AzureProject.DataAccess.UnitOfWork.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureProject.DataAccess.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private ICategoryDal _categoryRepository;
        private ISliderDal _sliderRepository;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public ICategoryDal CategoryRepository
        {
            get
            {
                return _categoryRepository ??= new CategoryRepository(_context);
            }
        }
        public ISliderDal SliderRepository
        {
            get
            {
                return _sliderRepository ??= new SliderRepository(_context);
            }
        }
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync(); // This saves all changes at once
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
