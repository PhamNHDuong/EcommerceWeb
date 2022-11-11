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
        //private readonly UserManager<User> _userManager;
        //private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;

        public UsersController(/*UserManager<User> userManager, RoleManager<IdentityRole> roleManager, */IConfiguration configuration, IRepositoryWrapper repository, IMapper mapper)
        {
            //_userManager = userManager;
            //_roleManager = roleManager;
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
            /*
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var token = GetToken(authClaims);

                return Ok(new Token
                {
                    TokenString = new JwtSecurityTokenHandler().WriteToken(token),
                    Expiration = token.ValidTo,
                    UserInfo = new UserDTO { Id = user.Id.ToString(), Roles = (List<string>)userRoles, UserName = user.UserName },
                });
            }
            */
            var user = await _repository.User.GetByAsync(u => u.UserName == model.Username);
            if (user != null && user.Password == model.Password)
            {
                var authClaims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    };
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
            /*
            User user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            bool customerRoleExits = await _roleManager.RoleExistsAsync(UserRoles.Customer);
            if (!customerRoleExits)
            {
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Customer));
            }
            await _userManager.AddToRoleAsync(user, UserRoles.Customer);
            */
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
        [Route("register-admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel model)
        {
            //var userExists = await _userManager.FindByNameAsync(model.Username);
            //if (userExists != null)
            //    return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

            //User user = new()
            //{
            //    Email = model.Email,
            //    SecurityStamp = Guid.NewGuid().ToString(),
            //    UserName = model.Username
            //};
            //var result = await _userManager.CreateAsync(user, model.Password);
            //if (!result.Succeeded)
            //    return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });

            //if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
            //    await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            //if (!await _roleManager.RoleExistsAsync(UserRoles.Customer))
            //    await _roleManager.CreateAsync(new IdentityRole(UserRoles.Customer));

            //if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
            //{
            //    await _userManager.AddToRoleAsync(user, UserRoles.Admin);
            //}
            //if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
            //{
            //    await _userManager.AddToRoleAsync(user, UserRoles.Customer);
            //}
            return Ok(new Response { Status = "Success", Message = "User updated successfully!" });
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

        [HttpPatch("show")]
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

        [HttpPatch("delete")]
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

