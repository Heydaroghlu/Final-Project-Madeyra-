using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Madeyra.Areas.AdminPanel.ViewModels
{
    public class AdminViewModel
    {
        [Required]
        [StringLength(maximumLength: 25, MinimumLength = 3, ErrorMessage = "Ad uzunluğu min:5, max:25")]
        public string Name { get; set; }
        [Required]
        [StringLength(maximumLength: 25, MinimumLength = 5, ErrorMessage = "Soyad uzunluğu min:10, max:25")]
        public string Surname { get; set; }
        [Required]
        [StringLength(maximumLength: 100, ErrorMessage = "Nomrenin uzunluqu max:100")]
        public string PhoneNumber { get; set; }
        [Required]
        [StringLength(maximumLength: 100, ErrorMessage = "Email uzunluqu max:100")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [StringLength(maximumLength: 25, MinimumLength = 5, ErrorMessage = "Şifrənin uzunluğu min:5, max:25")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [StringLength(maximumLength: 25, MinimumLength = 5, ErrorMessage = "Şifrənin uzunluğu min:5, max:25")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }

    }
}
