using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace user_management.DTOs.Group
{
    public class GroupUpdateDto
    {
        public string name { get; set; }=string.Empty;
        public string? description { get; set; }=string.Empty ;
    }
}