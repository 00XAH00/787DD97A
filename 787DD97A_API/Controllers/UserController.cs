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

        [HttpPost("Login")]
        public ActionResult<string> UserLogin([FromBody] UserLoginModel user_auth)
        {
            var user = _context.Users
                .Where(u => u.Email.Equals(user_auth.Username))
                .Include(ua => ua.UsersDevices)
                .FirstOrDefault();

            if (user_auth.DeviceId == "") { return BadRequest("DeviceId empty"); }
            if (!ValidatePassword(user_auth.Password, user)) { return Unauthorized("Password isn't correct"); }


            string token = _jwt.GenJWT(user.Email, 0);
            var refreshToken = GenerateRefreshToken();
            SetRefreshToken(refreshToken);


            var Device = user.UsersDevices.Where(u => u.DeviceId == user_auth.DeviceId).FirstOrDefault();
            if (Device is null)
            {
                UserDevice device = new UserDevice { TokenCreate = refreshToken.Created, TokenExpire = refreshToken.Expires, RefreshToken = refreshToken.Token, DeviceId = user_auth.DeviceId };
                user.UsersDevices.Add(device);
            }
            else
            {
                Device.RefreshToken = refreshToken.Token;
                Device.TokenCreate = refreshToken.Created;
                Device.TokenExpire = refreshToken.Expires;
            }
            _context.SaveChanges();
            return Ok(token);

        }

        [HttpPost("Register")]
        public ActionResult<User> UserRegister([FromBody] UserRegisterModel user_data)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            try
            {
                User user = new User
                {
                    Email = user_data.email,
                    FirstName = user_data.name,
                    SecondName = user_data.surname,
                    Password = Hash(user_data.password)
                };

                _context.Add(user);

                _context.SaveChanges();
                result.Add("ok", true);
                return Ok(user);
            }
            catch (DbUpdateException e)
            {
                result.Add("ok", false);

                if (e.InnerException.Message.Contains("Duplicate entry")) { result.Add("reason", "User already exists"); }
                else { result.Add("reason", e.InnerException.Message); }

                return BadRequest(result);
            }

        }

        [HttpPost("RefreshToken")]
        public ActionResult<string> RefreshToken([FromBody] TokenRefreshModel data)
        {
            var refreshToken = Request.Cookies["refreshToken"];
            var user = _context.Users
                .Where(u => u.uuid.Equals(data.uuid))
                .Include(ua => ua.UsersDevices)
                .FirstOrDefault();
            if (user is null) { return Unauthorized("Invalid user uuid."); }

            var Device = user.UsersDevices.Where(u => u.DeviceId.Equals(data.DeviceId)).FirstOrDefault();

            if (Device is null) { return Unauthorized("Invalid DeviceId."); }

            if (!Device.RefreshToken.Equals(refreshToken)) { return Unauthorized("Invalid Refresh Token."); }
            else if (Device.TokenExpire < DateTime.Now) { return Unauthorized("Token expired."); }

            string token = _jwt.GenJWT(user.Email, 0);
            var newRefreshToken = GenerateRefreshToken();
            SetRefreshToken(newRefreshToken);

            Device.RefreshToken = newRefreshToken.Token;
            Device.TokenCreate = newRefreshToken.Created;
            Device.TokenExpire = newRefreshToken.Expires;

            _context.SaveChanges();

            return Ok(token);
        }



        [NonAction]
        private RefreshTokenModel GenerateRefreshToken()
        {
            var refreshToken = new RefreshTokenModel
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expires = DateTime.Now.AddYears(1),
                Created = DateTime.Now
            };

            return refreshToken;
        }
        [NonAction]
        private void SetRefreshToken(RefreshTokenModel newRefreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = newRefreshToken.Expires
            };
            Response.Cookies.Append("refreshToken", newRefreshToken.Token, cookieOptions);
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

