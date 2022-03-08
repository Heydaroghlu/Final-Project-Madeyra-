using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Madeyra.Models
{
    public class Card:BaseEntity
    {
        [Required]
        [StringLength(maximumLength:100)]
        public string Text { get; set; }
        public string Image { get; set; }
        [NotMapped]
        public IFormFile CardImageFile { get; set; }

    }
}
