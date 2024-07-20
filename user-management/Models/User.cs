using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace user_management.Models
{
    public class User: IdentityUser
    {
        public string firstName { get; set; }=string.Empty;
        public string lastName { get; set; }=string.Empty;
        public DateTime regDate { get; set; }=DateTime.Now;
        
        public int? groupId { get; set; }

        public Group? Group{ get; set; }
    }
}