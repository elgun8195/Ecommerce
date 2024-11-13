using AzureProject.DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureProject.DataAccess.UnitOfWork.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryDal CategoryRepository { get; } // Add repository for Category
        ISliderDal SliderRepository { get; } // Add repository for Category
        Task SaveAsync(); // Async Save method
    }
}
