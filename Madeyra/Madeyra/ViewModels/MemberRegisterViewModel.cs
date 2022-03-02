using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Madeyra.ViewModels
{
    public class MemberRegisterViewModel
    {
        [Required]
        [StringLength(maximumLength: 20, MinimumLength = 3, ErrorMessage = "Adınızın uzunluqu min:3,max:20 olmalıdır")]
        public string Name { get; set; }
        [Required]
        [StringLength(maximumLength: 50, MinimumLength = 5, ErrorMessage = "SoyAdınızın uzunluqu min:5,max:30 olmalıdır")]
        public string Surname { get; set; }
        [Required]
        [StringLength(maximumLength: 50, MinimumLength = 5)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [StringLength(maximumLength: 30)]
        [Required]
        public string Telephone { get; set; }
        [StringLength(maximumLength: 100)]
        public string Adress { get; set; }
        [StringLength(maximumLength: 25, MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [StringLength(maximumLength: 25, MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
        public bool IsPersistent { get; set; } = false;




    }
}
