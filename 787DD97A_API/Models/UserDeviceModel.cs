using System;
using System.ComponentModel.DataAnnotations;

namespace _787DD97A_API.Models
{
    public class UserDevice
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(255)]
        public string? DeviceId { get; set; }
        [MaxLength(255)]
        public string? RefreshToken { get; set; }
        public DateTime TokenExpire { get; set; }
        public DateTime TokenCreate { get; set; }

        public Guid Useruuid { get; set; }
    }
}

