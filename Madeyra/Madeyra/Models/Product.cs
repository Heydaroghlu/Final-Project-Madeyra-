using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Madeyra.Models
{
    public class Product:BaseEntity
    {
        [StringLength(maximumLength: 50)]
        public string Name { get; set; }
       public string Preference { get; set; }
        public int DesignId { get; set; }
        public string Includes { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        public bool IsInterestFree { get; set; }
      
        public string Size { get; set; }
        public int SubCategoryId { get; set; }
        public List<ProductColor> ProductColors { get; set; }
        public List<ProductMatreal> ProductMatreals { get; set; }
        [NotMapped]
        public List<int> MatrealIds { get; set; } = new List<int>();
        [NotMapped]
        public List<int> ColorIds { get; set; }= new List<int>();
        public List<ProductImage> ProductImages { get; set; }
        [NotMapped]
        public IFormFile PosterImage { get; set; }
        [NotMapped]
        public List<IFormFile> ImageFiles { get; set; }
        [NotMapped]
        public List<int> ProductImageIds { get; set; }
       
        public SubCategory SubCategory { get; set; }
        
    }
}
