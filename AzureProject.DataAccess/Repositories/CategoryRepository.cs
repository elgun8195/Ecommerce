using AzureProject.DataAccess.Abstract;
using AzureProject.DataAccess.DAL;
using AzureProject.Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

public class CategoryRepository : ICategoryDal
{
    private readonly AppDbContext _context;
    private readonly DbSet<Category> _categories;

    public CategoryRepository(AppDbContext context)
    {
        _context = context;
        _categories = _context.Categories; // _object-i təyin et
    }

    public void Delete(Category category)
    {
        _categories.Remove(category);
        _context.SaveChanges();
    }

    public Category Get(Expression<Func<Category, bool>> filter)
    {
        return _categories.SingleOrDefault(filter);
    }

    public void Insert(Category category)
    {
        _categories.Add(category);
        _context.SaveChanges();
    }

    public List<Category> List()
    {
        return _categories.ToList();
    }

    public List<Category> List(Expression<Func<Category, bool>> filter)
    {
        return _categories.Where(filter).ToList();
    }

    public void Update(Category category)
    {
        _categories.Update(category);
        _context.SaveChanges();
    }
}
