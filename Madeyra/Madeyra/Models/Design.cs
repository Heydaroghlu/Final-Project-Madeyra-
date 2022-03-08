using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Madeyra.Models
{
    public class Design:BaseEntity
    {
        public string Name { get; set; }
        public List<Product>  Product { get; set; }
        public bool IsDeleted { get; set; }
    }
}
