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
    }
}
