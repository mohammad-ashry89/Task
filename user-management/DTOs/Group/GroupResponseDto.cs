using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using user_management.DTOs.User;
using user_management.Models;

namespace user_management.DTOs.Group
{
    public class GroupResponseDto
    {
        public int groupId { get; set; }
         public string name { get; set; }=string.Empty;
        public string? description { get; set; }=string.Empty ;
        public List<UserResponseDto> members { get; set; }
    }
}