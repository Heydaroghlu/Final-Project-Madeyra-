using Madeyra.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Madeyra.Areas.AdminPanel.ViewModels
{
    public class DashboardViewModel
    {
        public List<Order> Orders { get; set; }
        public List<Order> Finish { get; set; }
        public List<ProductComment> ProductComments { get; set; }
    }
}
