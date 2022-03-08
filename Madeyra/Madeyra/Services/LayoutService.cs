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
        public List<WishListViewModel> GetWishs()
        {
            AppUser member = null;
            if(_contextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                member = _userManager.Users.FirstOrDefault(x => x.UserName == _contextAccessor.HttpContext.User.Identity.Name && !x.IsAdmin);
            }
            List<WishListViewModel> wishes = new List<WishListViewModel>();
            if(member==null)
            {
                var itemStr = _contextAccessor.HttpContext.Request.Cookies["Product"];
                if(itemStr!=null)
                {
                    wishes = JsonConvert.DeserializeObject<List<WishListViewModel>>(itemStr);
                    foreach(var item in wishes)
                    {
                        Product product = _context.Products.Include(x => x.ProductImages).FirstOrDefault(x => x.Id == item.ProductId);
                        if(product!=null)
                        {
                            item.Name = product.Name;
                            item.Price = product.SalePrice;
                            item.Image = product.ProductImages.FirstOrDefault(x => x.IsPoster == true)?.Image;
                        }
                    }
                }
            }
            else
            {
                List<WishListItem> wishListItems =_context.WishListItems.Include(x=>x.Product).ThenInclude(x=>x.ProductImages)
                    .Where(x => x.AppUserId == member.Id).ToList();
                wishes = wishListItems.Select(x => new WishListViewModel
                { 
                ProductId=x.ProductId,
                Image=x.Product.ProductImages.FirstOrDefault(x=>x.IsPoster==true)?.Image,
                Name=x.Product.Name,
                Price=x.Product.SalePrice
                }).ToList();


            }
            return wishes;
        }

    }
}
