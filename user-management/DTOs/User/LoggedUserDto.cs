using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace user_management.DTOs.User
{
    public class LoggedUserDto
    {
        [Required]
        public required string UserName { get; set; }
        [Required]
        public required string email { get; set; }   
        public required string role { get; set; }
        public required string token { get; set; }
    }
}