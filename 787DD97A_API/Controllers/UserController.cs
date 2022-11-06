using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using _787DD97A_API.Services.User;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using _787DD97A_API.Classes;
using _787DD97A_API.Models;
using System.Text;


namespace _787DD97A_API.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;
        private ApplicationContext _context;
        private JWTClass _jwt;

        public UserController(IConfiguration configuration, IUserService userService, ApplicationContext context)
        {
            _jwt = new JWTClass(configuration);
            _configuration = configuration;
            _userService = userService;
            _context = context;
        }

        [Authorize, HttpGet]
        public ActionResult<User> GetMe()
        {
            var user = _context.Users.Where(u => u.Email == _userService.GetMyName()).FirstOrDefault();

            return Ok(user);
        }

        [Authorize, HttpGet("GetDevices")]
        public ActionResult<User> GetUserDevices()
        {
            var user = _context.Users
                .Where(u => u.Email.Equals(_userService.GetMyName()))
                .Include(u => u.UsersDevices)
                .FirstOrDefault();

            return Ok(user.UsersDevices);
        }

        [Authorize, HttpPost("ChangePassword")]
        public ActionResult<string> ChangePassword([FromBody] ChangePasswordModel passwords)
        {
            var user = _context.Users.Where(u => u.Email.Equals(_userService.GetMyName())).FirstOrDefault();
            if (ValidatePassword(passwords.OldPassword, user))
            {
                string PasswordNewHash = Hash(passwords.NewPassword);
                user.Password = PasswordNewHash;
                _context.SaveChanges();
                return Ok("Password change");
            }

            return Unauthorized("password isn't correct");
        }


        [NonAction]
        public bool ValidatePassword(string password, User user)
        {
            string Password = Hash(password);

            if (Password == user.Password) { return true; }
            else { return false; }
        }
        [NonAction]
        private string Hash(string password)
        {
            var data = Encoding.UTF8.GetBytes(password);
            SHA512 shaM = new SHA512Managed();

            return BitConverter.ToString(shaM.ComputeHash(data));
        }

    }
}

