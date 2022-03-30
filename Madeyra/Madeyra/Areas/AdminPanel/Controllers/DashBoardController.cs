using Madeyra.Areas.AdminPanel.ViewModels;
using Madeyra.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Madeyra.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class DashBoardController : Controller
    {
        private readonly MContext _context;
        public DashBoardController(MContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            if(!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("login", "account");
            }
            DashboardViewModel dashboardVM = new DashboardViewModel
            {
                ProductComments = _context.ProductComments.Include(x => x.AppUser).ToList(),
                Finish = _context.Orders.Include(x => x.OrderItems).ThenInclude(x => x.Product).Where(x => x.Status == Enums.OrderStatus.Qəbul).ToList(),
                Orders=_context.Orders.Include(x=>x.OrderItems).ToList()
            };

            return View(dashboardVM);
        }
        public JsonResult Jesonforchart()
        {
            return Json(Chart());
        }

        public List<ChartViewModel> Chart()
        {
            List<ChartViewModel> salesStatistics = new List<ChartViewModel>();
            List<Product> products = _context.Products.OrderByDescending(x => x.SoldOut * (x.DiscountPrice > 0 ? x.SalePrice * (1 - x.DiscountPrice / 100) : x.SalePrice)).ToList();
            foreach (var item in products)
            {
                ChartViewModel salesStatisticsItem = new ChartViewModel
                {
                    Name = item.Name,
                    TotalAmount = (item.SoldOut * (item.DiscountPrice > 0 ?item.SalePrice * (1 - item.DiscountPrice / 100) -item.CostPrice: item.SalePrice-item.CostPrice))
                };
                salesStatistics.Add(salesStatisticsItem);
            }
            return salesStatistics;
        }
    }
}
