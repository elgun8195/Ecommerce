using AzureProject.Business.Concrete;
using AzureProject.DataAccess;
using AzureProject.DataAccess.DAL;
using AzureProject.Entity.Concrete;
using AzureProject.Entity.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AzureProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryManager _categoryManager;
        //CategoryManager categoryManager = new CategoryManager(new EfCategoryDal());
        public CategoryController(CategoryManager categoryManager)
        {
            _categoryManager = categoryManager;
        }

        [HttpGet("")]
        public IActionResult GetAll()
        {
            var categories = _categoryManager.GetList();
            return Ok(categories);
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var categories = _categoryManager.GetByIdCategory(id);
            if (categories == null) { return NotFound(); }
            return Ok(categories);
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, Category categoryReturnDto)
        {
            var existCategory = _categoryManager.GetByIdCategory(id);

            if (existCategory == null)
            {
                return NotFound();
            }
            if (_categoryManager.GetList().Any(x => x.Name.ToUpper().Trim() == categoryReturnDto.Name.ToUpper().Trim() && x.Id != id))
            {
                return StatusCode(409);
            }
            existCategory.Name = categoryReturnDto.Name;
            existCategory.UpdatedTime = DateTime.UtcNow;
            _categoryManager.CategoryUpdate(categoryReturnDto);
            return NoContent();
        }
        [HttpPost("")]
        public IActionResult Create(Category category)
        {
            if (_categoryManager.GetList().Any(g => g.Name.ToUpper().Trim() == category.Name.Trim().ToUpper()))
                return StatusCode(409);
            _categoryManager.CategoryAdd(category);
            return StatusCode(201, category);
        }
    }
}
