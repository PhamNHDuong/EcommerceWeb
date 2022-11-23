using AutoMapper;
using EcommerceWeb.API.Repositories;
using EcommerceWeb.API.Repositories.Interfaces;
using EcommerceWeb.Data.Entities;
using EcommerceWeb.Dto.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Services.Users;

namespace EcommerceWeb.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {        
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;

        public CategoriesController(IRepositoryWrapper repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetAllCategories()
        {
            var data = await _repository.Category.GetAll().ToListAsync();
            var categories = _mapper.Map<List<CategoryDto>>(data);

            return categories;
        }

        [HttpGet]
        [Route("available")]
        public async Task<ActionResult<IEnumerable<CategoryListDto>>> GetAvailableCategories()
        {
            var data = await _repository.Category.GetMany(c => c.IsDeleted == false).ToListAsync();
            var categories = _mapper.Map<List<CategoryListDto>>(data);

            return categories;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<CategoryDto>> GetCategoryById(string id)
        {
            var data = await _repository.Category.GetByAsync(u => u.CategoryId == Guid.Parse(id));
            if (data == null)
            {
                return NotFound();
            }
            var convertData = _mapper.Map<CategoryDto>(data);
            return Ok(convertData);
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateCategory([FromForm] CategoryCreateDto newCategory)
        {
            Category data = new Category()
            {
                CategoryId = Guid.NewGuid(),
                Name = newCategory.Name,
                Description = newCategory.Description
            };
            if (_repository.Category.InsertAsync(data).IsCanceled)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Category creation failed! Please check name and try again." });
            await _repository.SaveAsync();
            return Ok(new Response { Status = "Success", Message = "Category created successfully!" });
        }

        [HttpPut("update")]
        public async Task<ActionResult<CategoryDto>> UpdateCategory(Guid id, [FromBody] CategoryEditDto editedCategory)
        {
            var data = _repository.Category.GetByAsync(u => u.CategoryId == id);
            if (data.Result == null)
            {
                return NotFound();
            }

            Category category = data.Result;

            category.Name = editedCategory.Name;
            category.Description = editedCategory.Description;

            await _repository.Category.UpdateAsync(category);
            await _repository.SaveAsync();

            return Ok(new Response { Status = "Success", Message = "Category updated successfully!" });
        }

        [HttpPatch("enable")]
        public async Task<ActionResult<CategoryDto>> ShowCategory(string id)
        {
            var data = _repository.Category.GetByAsync(u => u.CategoryId == Guid.Parse(id));
            if (data.Result == null)
            {
                return NotFound();
            }

            Category category = data.Result;
            category.IsDeleted = false;

            await _repository.Category.UpdateAsync(category);
            await _repository.SaveAsync();

            return Ok(new Response { Status = "Success", Message = "Category showed successfully!" });
        }

        [HttpPatch("disable")]
        public async Task<ActionResult<CategoryDto>> DeleteCategory(string id)
        {
            var data = _repository.Category.GetByAsync(u => u.CategoryId == Guid.Parse(id));
            if (data.Result == null)
            {
                return NotFound();
            }

            Category category = data.Result;

            category.IsDeleted = true;

            await _repository.Category.UpdateAsync(category);
            await _repository.SaveAsync();

            return Ok(new Response { Status = "Success", Message = "Category deleted successfully!" });
        }
    }
}
