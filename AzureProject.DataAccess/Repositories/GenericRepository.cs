using AzureProject.DataAccess.Abstract;
using AzureProject.DataAccess.DAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AzureProject.DataAccess.Repositories
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext _context; // AppDbContext inject edilir
        private readonly DbSet<T> _object;

        // Constructor vasitəsilə DbContext inject edilir
        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _object = _context.Set<T>();
        }

        public void Delete(T entity)
        {
            // Entity state-nu Remove metodu ilə təyin edirik
            _object.Remove(entity);
            _context.SaveChanges();
        }

        public T Get(Expression<Func<T, bool>> filter)
        {
            // Verilən filter-ə uyğun bir elementi qaytar
            return _object.SingleOrDefault(filter);
        }

        public void Insert(T entity)
        {
            // Entity state-nu Add metodu ilə təyin edirik
            _object.Add(entity);
            _context.SaveChanges();
        }

        public List<T> List()
        {
            // Bütün obyekti geri qaytar
            return _object.ToList();
        }

        public List<T> List(Expression<Func<T, bool>> filter)
        {
            // Filter ilə uyğun olan obyektləri geri qaytar
            return _object.Where(filter).ToList();
        }

        public void Update(T entity)
        {
            // Entity state-nu Update metodu ilə təyin edirik
            _object.Update(entity);
            _context.SaveChanges();
        }
    }
}
