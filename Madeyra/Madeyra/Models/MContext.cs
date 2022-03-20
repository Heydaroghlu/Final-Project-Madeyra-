using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Madeyra.Models
{
    public class MContext:IdentityDbContext
    {
        public MContext(DbContextOptions<MContext>options):base(options)
        {

        }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Store> Stores { get; set; }

        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Design> Designs { get; set; }
        public DbSet<Matreal> Matreals { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet <Slider> Sliders { get; set; }
        public DbSet <Video> Videos { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ProductComment> ProductComments { get; set; }
        public DbSet<Campagain> Campagains { get; set; }
    }
}
