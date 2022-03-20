using Madeyra.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Madeyra.ViewModels
{
    public class HomeViewModel
    {
        public Setting Settings { get; set; }
        public List<Product> Products { get; set; }
        public List<Card> Cards { get; set; }
        public List<Order> EndOrders { get; set; }
        public List<Slider> Sliders { get; set; }

    }
}
