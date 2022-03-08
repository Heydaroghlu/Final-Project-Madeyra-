using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Madeyra.Models
{
    public class Matreal:BaseEntity
    {
        [StringLength(maximumLength: 20)]
        public string Name { get; set; }
        public List<Product> Products { get; set; }
        public bool IsDeleted { get; set; }
    }
}
