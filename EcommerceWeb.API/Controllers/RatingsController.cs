using AutoMapper;
using EcommerceWeb.API.Repositories.Interfaces;
using EcommerceWeb.Data.Entities;
using EcommerceWeb.Dto.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EcommerceWeb.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingsController : ControllerBase
    {
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;

        public RatingsController(IMapper mapper, IRepositoryWrapper repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(RatingDto model)
        {
            try
            {
                //await _repository.Rating.SaveAsync(new Rating { Product = SelectList(model.ProductId, Product), UserId = model.UserId, Comment = model.Comment, Star = model.Star });
                await _repository.SaveAsync();
            }
            catch (Exception e)
            {
                return BadRequest(new { message = "You have reviewed this product already" });
            }
            return Ok();
        }

        [HttpGet]
        public async Task<List<RatingDto>> Get(Guid id)
        {
            var result = await _repository.Rating.GetMany(r => r.Product.ProductId.Equals(id)).ToListAsync();
            if (result.Count == 0)
            {
                return new List<RatingDto>();
            }
            else
            {
                return _mapper.Map<List<RatingDto>>(result);
            }
        }
    }
}

