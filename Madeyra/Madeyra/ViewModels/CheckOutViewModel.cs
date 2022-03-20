using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Madeyra.ViewModels
{
    public class CheckOutViewModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public decimal CostPrice { get; set; }
        public int Count { get; set; }
        public string Image { get; set; }
        public string Category { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }

        public string Adress { get;set;}
        public string Phone { get; set; }
    }
}
