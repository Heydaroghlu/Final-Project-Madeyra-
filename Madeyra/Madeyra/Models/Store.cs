using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Madeyra.Models
{
    public class Store:BaseEntity
    {
        public string Name { get; set; }
        public string Tel { get; set; }
        public string Address { get; set; }
    }
}
