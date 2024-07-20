using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace user_management.DTOs.User
{
    public class UserResponseDto
    {
        public string firstName { get; set; }=string.Empty;
        public string lastName { get; set; }=string.Empty;
        public string email { get; set; }=string.Empty;
        public int? groupId { get; set; }
        public string username { get; set; }=string.Empty;

    }
}