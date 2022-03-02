using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Madeyra.ViewModels
{
    public class MemberLoginViewModel
    {
        [StringLength(maximumLength: 50, MinimumLength = 3, ErrorMessage = "Username must be min:3, max:50")]
        public string Email { get; set; }
        [StringLength(maximumLength: 25, MinimumLength = 6, ErrorMessage = "Password must be min:5, max:50")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool IsPersistent { get; set; }

    }
}
