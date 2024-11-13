using AzureProject.DataAccess.Abstract;
using AzureProject.DataAccess.DAL;
using AzureProject.DataAccess.Repositories;
using AzureProject.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureProject.DataAccess.EF
{
    public class EfSliderDal : GenericRepository<Slider>, ISliderDal
    {
        public EfSliderDal(AppDbContext context) : base(context)
        {
        }
    }
}
