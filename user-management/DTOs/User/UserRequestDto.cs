using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace user_management.DTOs.User
{
    public class UserRequestDto
    {
        [Required]
        public string firstName { get; set; }=string.Empty;
        [Required]
        public string lastName { get; set; }=string.Empty;
        [Required]
        [EmailAddress]
        public string email { get; set; }=string.Empty;
        [Required]
        public string username { get; set; }=string.Empty;
        [Required]
        public string password { get; set; }=string.Empty ;
    }
}