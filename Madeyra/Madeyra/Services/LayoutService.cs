using Madeyra.Models;
using Madeyra.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Madeyra.Services
{
    public class LayoutService
    {
        private readonly MContext _context;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UserManager<AppUser> _userManager;
        public LayoutService(MContext context,IHttpContextAccessor contextAccessor,UserManager<AppUser> userManager)
        {
            _userManager = userManager;
            _contextAccessor = contextAccessor;
            _context = context;
        }
        public Setting GetSetting()
        {
            return _context.Settings.FirstOrDefault();
        }
        public CataloqViewModel GetCataloq()
        {
            CataloqViewModel cataloqView = new CataloqViewModel
            {
                Categories = _context.Categories.Include(x=>x.SubCategories).ToList(),
                SubCategories = _context.SubCategories.ToList()

            };
            return cataloqView;
        }
       public List<BasketViewModel> GetBasket()
        {
            AppUser member = null;
            if(_contextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                member = _userManager.Users.FirstOrDefault(x => x.UserName == _contextAccessor.HttpContext.User.Identity.Name && !x.IsAdmin);

            }
            List<BasketViewModel> baskets = new List<BasketViewModel>();
            if(member==null)
            {
                var itemStr = _contextAccessor.HttpContext.Request.Cookies["Basket"];
                if(itemStr!=null)
                {
                    baskets = JsonConvert.DeserializeObject<List<BasketViewModel>>(itemStr);
                  
                }
            }
            else
            {
                List<BasketItem> basketItems = _context.BasketItems.Include(x => x.Product).ThenInclude(x => x.ProductImages)
                    .Where(x => x.AppUserId == member.Id).ToList();
                baskets = basketItems.Select(x => new BasketViewModel
                {
                    ProductId = x.ProductId,
                    Count = x.Count,
                    Image = x.Product.ProductImages.FirstOrDefault(x => x.IsPoster == true)?.Image,
                    ProductName = x.Product.Name,
                    Price = x.Product.SalePrice
                }).ToList();
            }
            return baskets;
        }
        public List<ProductComment> Comments()
        {
            return _context.ProductComments.Where(x=>x.Status==false).ToList();
        }
        public List<Product> GetProducts()
        {
            return _context.Products.Include(x => x.ProductImages).Take(0).ToList();
        }
       

    }
}
