using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceWeb.Dto.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "User Name is required")]
        public string? Username { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
    }
    public class LoginModel
    {
        [Required(ErrorMessage = "User Name is required")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
    }
    public static class UserRoles
    {
        public const string Admin = "Admin";
        public const string Customer = "Customer";
    }
    public class UserDTO
    {
        public UserDTO() { }

        public string Id { get; set; }
        public string UserName { get; set; }
        public List<string> Roles { get; set; }
    }
    public class Token
    {
        public string TokenString { get; set; }
        public DateTime Expiration { get; set; }
        public UserDTO UserInfo { get; set; }
    }
    public class Response
    {
        public string? Status { get; set; }
        public string? Message { get; set; }
    }
}
