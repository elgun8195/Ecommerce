using AzureProject.DataAccess.Abstract;
using AzureProject.DataAccess.DAL;
using AzureProject.Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;


public class SliderRepository : ISliderDal
{
    private readonly AppDbContext _context;
    private readonly DbSet<Slider> _sliders;

    public SliderRepository(AppDbContext context)
    {
        _context = context;
        _sliders = _context.Sliders; // _object-i təyin et
    }

    public void Delete(Slider slider)
    {
        _sliders.Remove(slider);
        _context.SaveChanges();
    }

    public Slider Get(Expression<Func<Slider, bool>> filter)
    {
        return _sliders.SingleOrDefault(filter);
    }

    public void Insert(Slider slider)
    {
        _sliders.Add(slider);
        _context.SaveChanges();
    }

    public List<Slider> List()
    {
        return _sliders.ToList();
    }

    public List<Slider> List(Expression<Func<Slider, bool>> filter)
    {
        return _sliders.Where(filter).ToList();
    }

    public void Update(Slider slider)
    {
        _sliders.Update(slider);
        _context.SaveChanges();
    }
}
