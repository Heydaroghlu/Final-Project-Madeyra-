using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Madeyra.Models
{
    public class AppUser:IdentityUser
    {
        [StringLength(maximumLength:20)]
        public string Name { get; set; }
        [StringLength(maximumLength: 30)]
        public string Surname { get; set; }
        [StringLength(maximumLength: 100)]
        public string Adress { get; set; }
        public bool IsAdmin { get; set; }
    }
}
