using Madeyra.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Madeyra.ViewModels
{
    public class ProductDetailViewModel
    {
        public Product Product { get; set; }
        public List<Product> News { get; set; }
        public ProductComment ProductComment { get; set; }
        public AppUser User { get; set; }
        public List<Order> EndOrders { get; set; }
    }
}
