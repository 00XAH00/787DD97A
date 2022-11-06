using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace _787DD97A_API.Models
{
    public class User
    {
        [Key]
        public Guid uuid { get; set; }
        [MaxLength(30)]
        public string? FirstName { get; set; }
        [MaxLength(30)]
        public string? SecondName { get; set; }
        [MaxLength(255)]
        public string? Email { get; set; }
        [MaxLength(255)]
        public string? Password { get; set; }

        [ForeignKey("Useruuid")]
        public List<UserDevice> UsersDevices { get; set; } = new();
    }
}

