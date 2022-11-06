using System;
namespace _787DD97A_API.Models
{
    public class RefreshTokenModel
    {
        public string Token { get; set; } = string.Empty;
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime Expires { get; set; }
    }
}

