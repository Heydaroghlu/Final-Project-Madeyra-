using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Madeyra.Models
{
    public class Category:BaseEntity
    {
        [StringLength(maximumLength: 60)]
        public string Name { get; set; }
        public List<SubCategory> SubCategories { get; set; }
        public bool IsDeleted { get; set; }
    }
}
