using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Madeyra.Models
{
    public class Setting:BaseEntity
    {
        [StringLength(maximumLength: 150)]
        public string HeaderLogo { get; set; }
        [StringLength(maximumLength: 150)]
        public string FooterLogo { get; set; }
        [StringLength(maximumLength: 70)]
        public string TelIcon { get; set; }
        [StringLength(maximumLength: 50)]
        public string Tel { get; set; }
        [StringLength(maximumLength: 70)]
        public string FacebookIcon { get; set; }
        [StringLength(maximumLength: 150)]
        public string FacebookUrl { get; set; }
        [StringLength(maximumLength: 70)]
        public string YoutubeIcon { get; set; }
        [StringLength(maximumLength: 150)]
        public string YoutubeUrl { get; set; }
        [StringLength(maximumLength: 70)]
        public string InstagramIcon { get; set; }
        [StringLength(maximumLength: 150)]
        public string InstagramUrl { get; set; }
        [StringLength(maximumLength: 70)]
        public string WhatsappIcon { get; set; }
        [StringLength(maximumLength: 150)]
        public string WhatsappUrl { get; set; }
        [StringLength(maximumLength:150)]
        public string Adress { get; set; }
        [StringLength(maximumLength: 50)]
        public string Email { get; set; }
        [StringLength(maximumLength: 70)]
        public string Faks { get; set; }
        public string Reclam1 { get; set; }
        public string Reclam2 { get; set; }
        [NotMapped]
        public IFormFile ReclamFile1 { get; set; }
        [NotMapped]
        public IFormFile ReclamFile2 { get; set; }

        [NotMapped]
        public IFormFile HeaderLogoFile { get; set; }
        [NotMapped]
        public IFormFile FooterLogoFile { get; set; }


    }
}
