using AutoMapper;
using EcommerceWeb.API.Repositories;
using EcommerceWeb.API.Repositories.Interfaces;
using EcommerceWeb.Data.Entities;
using EcommerceWeb.Dto.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcommerceWeb.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;

        public ProductsController(IRepositoryWrapper repository, IMapper mapper)
        {
            _repository = repository; 
            _mapper = mapper;
        }

        // GET: api/Products
        [HttpGet("Test")]
        public async Task<ActionResult> GetProducts()
        {
            try
            {
                var products = await _repository.Product.GetAllProductsAsync();
                //_logger.LogInformation($"Returned all owners from database.");
                var data = _mapper.Map<IEnumerable<ProductDto>>(products);
                //return Ok(ownersResult);
                return Ok(data);
            }
            catch (Exception ex)
            {
                //_logger.LogError($"Something went wrong inside GetAllOwners action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
            //return await _context.Products.ToListAsync();
        }

        [HttpGet]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult<ViewListDto<ProductDto>>> GetAllProducts([FromQuery] int pageIndex)
        {
            try
            {
                var productsData = await _repository.Product.PagingAsync(_repository.Product.GetAll().OrderBy(x => x.IsDeleted ? 1 : 0).Include(x => x.Category), pageIndex);

                var products = _mapper.Map<List<ProductDto>>(productsData.ModelDatas);

                return Ok(new ViewListDto<ProductDto> { ModelDatas = products, MaxPage = productsData.MaxPage, PageIndex = pageIndex });
            }
            catch (IndexOutOfRangeException ex)
            {
                return BadRequest("Can't find this page");
            }
        }

        [HttpGet("available")]
        public async Task<ActionResult<ViewListDto<ProductDto>>> GetAvailableProducts([FromQuery] int pageIndex)
        {
            try
            {
                var productsData = await _repository.Product.PagingAsync(_repository.Product.GetMany(p => p.IsDeleted == false, p => p.Category).OrderBy(p => p.Name), pageIndex);

                var products = _mapper.Map<List<ProductDto>>(productsData.ModelDatas);

                return Ok(new ViewListDto<ProductDto> { ModelDatas = products, MaxPage = productsData.MaxPage, PageIndex = pageIndex });
            }
            catch (IndexOutOfRangeException ex)
            {
                return BadRequest("Can't find this page");
            }
        }
        //User search
        [HttpPost("search")]
        public async Task<ActionResult<ViewListDto<ProductDto>>> GetProductByName([FromBody] RequestSearchProductDTO requestModel)
        {
            try
            {
                var responseData = new ViewListDto<Product>();
                if (string.IsNullOrEmpty(requestModel.ProductName) && requestModel.CategoryId.Equals(Guid.Empty))
                {
                    return NotFound();
                }
                else if (string.IsNullOrEmpty(requestModel.ProductName))
                {
                    responseData = await _repository.Product.PagingAsync(_repository.Product.GetMany(x => x.Category.CategoryId.Equals(requestModel.CategoryId) && x.IsDeleted == false, p => p.Category), requestModel.PageIndex);
                }
                else if (requestModel.CategoryId.Equals(Guid.Empty))
                {
                    responseData = await _repository.Product.PagingAsync(_repository.Product.GetMany(x => x.Name.ToUpper().Contains(requestModel.ProductName.ToUpper()) && x.IsDeleted == false, x => x.Category), requestModel.PageIndex);
                }
                else
                {
                    responseData = await _repository.Product.PagingAsync(_repository.Product.GetMany(x => x.Name.ToUpper().Contains(requestModel.ProductName.ToUpper()) && x.Category.CategoryId.Equals(requestModel.CategoryId) && x.IsDeleted == false, x => x.Category), requestModel.PageIndex);
                }

                if (responseData == null)
                {
                    return BadRequest(new { message = "Can't find" });
                }

                var products = _mapper.Map<List<ProductDto>>(responseData.ModelDatas);

                return Ok(new ViewListDto<ProductDto> { ModelDatas = products, MaxPage = responseData.MaxPage, PageIndex = requestModel.PageIndex });
            }
            catch (IndexOutOfRangeException ex)
            {
                return BadRequest("Can't find this page");
            }
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProduct([FromRoute] Guid id)
        {
            var product = await _repository.Product.GetByAsync(p => p.ProductId == id);

            if (product == null)
            {
                return NotFound();
            }
            var convertData = _mapper.Map<ProductDto>(product);

            return convertData;
        }

    }
}
