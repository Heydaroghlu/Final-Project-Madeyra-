using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Madeyra.Models
{
    public class ProductComment:BaseEntity
    {
        public string AppUserId { get; set; }
        public DateTime SendTime { get; set; }
        public int ProductId { get; set; }
        [Required]
        [StringLength(maximumLength: 120)]
        public string Text { get; set; }
        public bool Status { get; set; }

        [StringLength(maximumLength: 20)]
        public string Name { get; set; }
        [StringLength(maximumLength: 20)]
        public string SurName { get; set; }

        public Product Product { get; set; }
        public AppUser AppUser { get; set; }
    }
}
