using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Madeyra.ViewModels
{
    public class WishListViewModel
    {
        public decimal Price { get; set; }
        public string Name { get; set; }
        public int ProductId { get; set; }
        public string Image { get; set; }
        public int Count { get; set; }
    }
}
