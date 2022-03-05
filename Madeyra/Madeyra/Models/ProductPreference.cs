using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Madeyra.Models
{
    public class ProductPreference
    {
        public int ProductId { get; set; }
        public int PreferenceId { get; set; }
        public Product Product { get; set; }
        public Preference Preference { get; set; }
    }
}
