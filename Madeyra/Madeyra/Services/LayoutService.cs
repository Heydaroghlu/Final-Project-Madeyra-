using Madeyra.Models;
using Madeyra.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Madeyra.Services
{
    public class LayoutService
    {
        private readonly MContext _context;
        public LayoutService(MContext context)
        {
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

    }
}
