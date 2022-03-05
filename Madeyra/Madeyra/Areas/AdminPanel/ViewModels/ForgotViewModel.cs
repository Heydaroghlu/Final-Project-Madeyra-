using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Madeyra.Areas.AdminPanel.ViewModels
{
    public class ForgotViewModel
    {
        [Required]
        [StringLength(maximumLength:100)]
        public string Email { get; set; }
    }
}
