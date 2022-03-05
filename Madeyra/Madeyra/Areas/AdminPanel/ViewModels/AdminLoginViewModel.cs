using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Madeyra.Areas.AdminPanel.ViewModels
{
    public class AdminLoginViewModel
    {
        [Required]
        [StringLength(maximumLength:50,MinimumLength =5)]
        public string Username { get; set; }
        [Required]
        [StringLength(maximumLength: 25, MinimumLength = 6)]
        public string Password { get; set; }
    }
}
