using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Madeyra.Models
{
    public class ProductMatreal:BaseEntity
    {
        public int ProductId { get; set; }
        public int MatrealId { get; set; }
        public Product Product { get; set; }
        public Matreal Matreal { get; set; }
    }
}
