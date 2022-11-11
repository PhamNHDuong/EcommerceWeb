using AutoMapper;
using EcommerceWeb.API.Repositories.Interfaces;
using EcommerceWeb.API.Utilities;
using EcommerceWeb.Data.Entities;
using EcommerceWeb.Dto.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EcommerceWeb.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;
        private readonly ImageExtensions _img;

        public ImagesController(IRepositoryWrapper repository, IMapper mapper, ImageExtensions image)
        {
            _repository = repository;
            _mapper = mapper;
            _img = image;
        }


        // GET: api/<ImagesController>
        [HttpGet]
        public async Task<IActionResult> GetAllImages()
        {
            try
            {
                var images = await _repository.Image.GetAll().ToListAsync();
                //var data = _mapper.Map<IEnumerable<ProductDto>>(products);
                //return Ok(ownersResult);
                return Ok(images);
            }
            catch (Exception ex)
            {
                //_logger.LogError($"Something went wrong inside GetAllOwners action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
            
        [HttpGet("{id}")]
        public async Task<ActionResult<List<ProductImageDto>>> GetImages([FromRoute] Guid id)
        {
            var data = _repository.Image.GetMany(p => p.Product.ProductId == id, p => p.Product);

            if (data == null)
            {
                return NotFound();
            }
            var convertData = _mapper.Map<List<ProductImageDto>>(data);
            return convertData;
        }

        [HttpPost]
        [Route("image")]
        public async Task<IActionResult> ImageTest()
        {
            string path = @"C:\Users\Admin\Pictures\Testimages\images.jpg";
            byte[] b = _img.ImageToByteArray(Image.FromFile(path));
            ProductImage img = new ProductImage()
            {
                Product = await _repository.Product.GetByAsync(p => p.ProductId.Equals("501b3b6d-eeb0-4b07-a9ac-e312e08bddeb")),
                ImageId = Guid.Parse("501b3b6d-eeb0-4b27-a9ac-e312e08bdde2"),
                ImageBin = b,
            };
            if (_repository.Image.InsertAsync(img).IsCanceled)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Product creation failed! Please check product details and try again." });
            await _repository.SaveAsync();
            return Ok(new Response { Status = "Success", Message = "Product created successfully!" });
        }

        [HttpPut("update/img")]
        public async Task<ActionResult<ProductDto>> UpdateImg()
        {
            var data = _repository.Image.GetByAsync(u => u.ImageId == Guid.Parse("501b3b6d-eeb0-4b27-a9ac-e312e08bdde2"));
            if (data.Result == null)
            {
                return NotFound();
            }

            string path = @"C:\Users\Admin\Pictures\Testimages\images.jpg";

            var img = data.Result;
            byte[] b = _img.ImageToByteArray(Image.FromFile(path));
            img.ImageBin = b;
            img.Product = await _repository.Product.GetByAsync(p => p.ProductId.Equals("501b3b6d-eeb0-4b07-a9ac-e312e08bddeb"));

            await _repository.Image.UpdateAsync(img);
            await _repository.SaveAsync();

            return Ok(new Response { Status = "Success", Message = "Product updated successfully!" });
        }
    }
}
