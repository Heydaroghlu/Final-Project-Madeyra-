using Madeyra.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace Madeyra.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [Authorize(Roles ="Admin,SuperAdmin")]
    public class OrderController : Controller
    {
        private readonly MContext _context;
        public OrderController(MContext context)
        {
            _context = context;
        }
        public IActionResult Index(int page = 1, int deleted = 0, string? search = null)
        {
            var orders = _context.Orders.Include(x => x.OrderItems).AsQueryable();
            if (deleted == 0)
            {
                orders = orders;
            }
            if (deleted == 1)
            {
                orders = orders.Where(x => x.Status == Enums.OrderStatus.Gözləmədə);
            }
            else if (deleted == 2)
            {
                orders = orders.Where(x => x.Status == Enums.OrderStatus.Qəbul);
            }
            else if (deleted == 3)
            {
                orders = orders.Where(x => x.Status == Enums.OrderStatus.İmtina);
            }
            if (search != null)
            {
                orders = orders.Where(x => x.Name.Contains(search) || x.Surname.Contains(search) || x.Phone.Contains(search) || x.Address.Contains(search));
            }

            ViewBag.IsDeleted = deleted;
            ViewBag.Search = search;
            return View(orders.ToPagedList(page,10)
                );
        }
        public IActionResult Items(int id)
        {
            Order order = _context.Orders.Include(x => x.OrderItems)
                .ThenInclude(x => x.Product)
                .ThenInclude(x => x.ProductImages).FirstOrDefault(x=>x.Id==id);
            if(order==null)
            {
                return View("index", "error");
            }
            return View(order);
        }
        public IActionResult Accept(int id)
        {
            Order order = _context.Orders.Include(x=>x.OrderItems).FirstOrDefault(x => x.Id == id);
            if(order==null)
            {
                return RedirectToAction("index", "error");
            }
            if (order.OrderItems.Count == 0)
            {
                TempData["OrderError"] = "Sifarişin məhsul sayı 0 dır";
                return RedirectToAction("items", new { id = order.Id });

            }
            int dovr = 0;
            foreach (var item in order.OrderItems)
            {
                Product product = _context.Products.FirstOrDefault(x => x.Id == item.ProductId);
                 if (item.Count == 0 )
                {
                    dovr++;
                    if(dovr==order.OrderItems.Count)
                    {
                        TempData["OrderError"] = "Sifarişin məhsul sayı 0 dır";
                        return RedirectToAction("items", new { id = order.Id });
                    }

                }
                else if (product.Count >= item.Count)
                {
                    product.Count=product.Count-item.Count;
                    product.SoldOut += item.Count;
                    _context.SaveChanges();
                }
              
                else if (product.Count == 0)
                {

                    TempData["MehsulBitib"] = $"{item.ProdName} Bitib";

                    return RedirectToAction("items", new { id = order.Id });

                }
                else if (product.Count< item.Count)
                {
                    TempData["MehsulBitib2"] = $"{item.ProdName} sifarişi sayda Anbarda məhsul yoxdur. Max Sayı: {product.Count}";
                    return RedirectToAction("items", new { id = order.Id });

                }
               


            }
           
            order.Status = Enums.OrderStatus.Qəbul;
            _context.SaveChanges();
            return RedirectToAction("index");
        }
        public IActionResult OrderItemDelete(int id,int id2)
        {
            Order order = _context.Orders.Include(x=>x.OrderItems).FirstOrDefault(x => x.Id == id);
            if (order == null)
            {
                return View("index", "error");
            }
            OrderItem item = order.OrderItems.FirstOrDefault(x => x.Id == id2);
            if (item == null || item.Count==0 )
            {
                return RedirectToAction("index", "error");
            }
            item.Count--;
            _context.SaveChanges();
            return RedirectToAction("items", new { id = order.Id });

        }
        public IActionResult Reject(int id)
        {
            Order order = _context.Orders.FirstOrDefault(x => x.Id == id);
            if (order == null)
            {
                return RedirectToAction("index", "error");
            }
            order.Status = Enums.OrderStatus.İmtina;
            _context.SaveChanges();
            return RedirectToAction("index");
        }
        public IActionResult Waiting(int id)
        {
            Order order = _context.Orders.FirstOrDefault(x => x.Id == id);
            if (order == null)
            {
                return RedirectToAction("index", "error");
            }
            order.Status = Enums.OrderStatus.Gözləmədə;
            _context.SaveChanges();
            return RedirectToAction("index");
        }
        public IActionResult Depo(int id)
        {
            Order order = _context.Orders.FirstOrDefault(x => x.Id == id);
            if (order == null)
            {
                return RedirectToAction("index", "error");
            }
            order.DeliveryStatus = Enums.OrderDeliveryStatus.Anbarda;
            _context.SaveChanges();
            return RedirectToAction("index");
        }
        public IActionResult Curier(int id)
        {
            Order order = _context.Orders.FirstOrDefault(x => x.Id == id);
            if (order == null)
            {
                return RedirectToAction("index", "error");
            }
            order.DeliveryStatus = Enums.OrderDeliveryStatus.Kuryerde;
            _context.SaveChanges();
            return RedirectToAction("index");
        }
        public IActionResult Finish(int id)
        {
            Order order = _context.Orders.FirstOrDefault(x => x.Id == id);
            if (order == null)
            {
                return RedirectToAction("index", "error");
            }
            order.DeliveryStatus = Enums.OrderDeliveryStatus.Catdirildi;
            _context.SaveChanges();
            return RedirectToAction("index");
        }
    }
}
