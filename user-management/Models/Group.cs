using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace user_management.Models
{
    public class Group
    {
        public int groupId { get; set; }
       
        public string groupName { get; set; }=string.Empty;
        public string? description { get; set; }=string.Empty ;
        public List<User> members { get; set; }= new List<User>();
    }
}