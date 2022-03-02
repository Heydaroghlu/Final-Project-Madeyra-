using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Madeyra.ViewModels
{
    public class ProfileViewModel
    {
        [StringLength(maximumLength: 20, MinimumLength = 3, ErrorMessage = "Adınızın uzunluqu min:3,max:20 olmalıdır")]
        public string Name { get; set; }
        [StringLength(maximumLength: 30, MinimumLength = 5, ErrorMessage = "SoyAdınızın uzunluqu min:5,max:30 olmalıdır")]
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [StringLength(maximumLength: 120, MinimumLength = 5, ErrorMessage = "Adınızın uzunluqu min:3,max:20 olmalıdır")]
        public string Adress { get; set; }
        [StringLength(maximumLength: 25, MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }
        [StringLength(maximumLength: 25, MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [StringLength(maximumLength: 25, MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Compare(nameof(NewPassword))]
        public string NewPasswordConfirmed { get; set; }

    }
}
