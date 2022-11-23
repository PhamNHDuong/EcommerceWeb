using AutoMapper;
using EcommerceWeb.API.Repositories.Interfaces;
using EcommerceWeb.Data.Entities;
using EcommerceWeb.Dto.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NuGet.Protocol;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EcommerceWeb.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;

        public UsersController(IConfiguration configuration, IRepositoryWrapper repository, IMapper mapper)
        {
            _configuration = configuration;
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("getUsers")]
        public async Task<IActionResult> GetUsers()
        {
            var data = await _repository.User.GetAll().ToListAsync();
            if (data == null)
            {
                return NotFound();
            }
            var convertData = _mapper.Map<IEnumerable<UserDto>>(data);
            return Ok(convertData);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetUsersById(Guid id)
        {
            var data = await _repository.User.GetByAsync(u => u.AUserId == id);
            if(data == null)
            {
                return NotFound();
            }
            var convertData = _mapper.Map<IEnumerable<UserDto>>(data);
            return Ok(convertData);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _repository.User.GetByAsync(u => u.UserName == model.Username && u.IsDeleted == false);
            if (user != null && user.Password == model.Password)
            {
                var authClaims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    };
                authClaims.Add(new Claim(ClaimTypes.Role, user.Role));
                var token = GetToken(authClaims);

                return Ok(new Token
                {
                    TokenString = new JwtSecurityTokenHandler().WriteToken(token),
                    Expiration = token.ValidTo,
                    UserInfo = new UserDto { AUserId = user.AUserId, Role = user.Role, UserName = user.UserName },
                });
            }
            return Unauthorized();
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var userExists = await _repository.User.GetByAsync(u => u.UserName == model.Username);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });
            AUser user = new AUser()
            {
                AUserId = Guid.NewGuid(),
                UserName = model.Username,
                Password = model.Password,
                Role = "Customer",
                UserEmail = model.Email,
                
            };
            if (_repository.User.InsertAsync(user).IsCanceled)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });
            await _repository.SaveAsync();
            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }

        [HttpPost]
        [Route("admin-register")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel model)
        {
            return Ok(new Response { Status = "Success", Message = "User registered successfully!" });
        }

        [HttpPut("update")]
        public async Task<ActionResult<UserDto>> UpdateUser(Guid id,[FromBody] UserEditDto userUpdated)
        {
            var data = _repository.User.GetByAsync(u => u.AUserId == id);
            if (data.Result == null)
            {
                return NotFound();
            }

            AUser user = data.Result;

            user.UserEmail = userUpdated.UserName;
            user.UserName = userUpdated.UserName;

            await _repository.User.UpdateAsync(user);
            await _repository.SaveAsync();

            return Ok(new Response { Status = "Success", Message = "User deleted successfully!" });
        }

        [HttpPatch("enable")]
        public async Task<ActionResult<UserDto>> ShowUser(Guid id)
        {
            var data = _repository.User.GetByAsync(u => u.AUserId == id);
            if (data.Result == null)
            {
                return NotFound();
            }

            AUser user = data.Result;

            user.IsDeleted = false;

            await _repository.User.UpdateAsync(user);
            await _repository.SaveAsync();

            return Ok(new Response { Status = "Success", Message = "User showed successfully!" });
        }

        [HttpPatch("disable")]
        public async Task<ActionResult<UserDto>> DeleteUser(Guid id)
        {
            var data = _repository.User.GetByAsync(u => u.AUserId == id);
            if (data.Result == null)
            {
                return NotFound();
            }

            AUser user = data.Result;

            user.IsDeleted = true;

            await _repository.User.UpdateAsync(user);
            await _repository.SaveAsync();

            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }

        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }


    }
}

