using AutoMapper;
using EcommerceWeb.API.Repositories.Interfaces;
using EcommerceWeb.Data.Entities;
using EcommerceWeb.Dto.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.TeamFoundation.Common;

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
        //[Authorize]
        public async Task<IActionResult> Create(RatingDto model)
        {
            if (CheckIfExistRating(model.AUserAUserId, model.ProductProductId))
            {
                return BadRequest(new { message = "You have reviewed this product already" });
            }
            try
            {
                await _repository.Rating.InsertAsync(new Rating { 
                    Product = await _repository.Product.GetByAsync(p => p.ProductId == model.ProductProductId), 
                    AUser = await _repository.User.GetByAsync(p => p.AUserId == model.AUserAUserId),
                    Comment = model.Comment, 
                    Star = (RatingStar)model.Star 
                });
                await _repository.SaveAsync();
            }
            catch (Exception e)
            {
                return BadRequest(new { message = "You have reviewed this product already" });
            }
            return Ok(new Response { Status = "Success", Message = "Rating created successfully!" });
        }

        [HttpGet]
        public async Task<List<RatingDto>> Get(Guid id)
        {
            var result = await _repository.Rating.GetMany(r => r.Product.ProductId.Equals(id), r => r.AUser).Include(r => r.Product).ToListAsync();
            if (result.Count == 0)
            {
                return new List<RatingDto>();
            }
            else
            {
                return _mapper.Map<List<RatingDto>>(result);
            }
        }

        [HttpGet]
        [Route("getAll")]
        public async Task<List<RatingDto>> GetAll()
        {
            var result = await _repository.Rating.GetAllRatingsAsync();
            return _mapper.Map<List<RatingDto>>(result);
        }

        private bool CheckIfExistRating(Guid AUserid, Guid ProductId)
        {
            var existRating = _repository.Rating.GetMany(r => r.AUser.AUserId == AUserid && r.Product.ProductId == ProductId, r => r.AUser).Include(r => r.Product);
            return (!existRating.IsNullOrEmpty());
        }
    }
}

