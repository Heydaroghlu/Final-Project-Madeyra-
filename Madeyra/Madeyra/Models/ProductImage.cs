using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Madeyra.Models
{
    public class ProductImage:BaseEntity
    {
        public string Image { get; set; }
        public bool IsPoster { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
