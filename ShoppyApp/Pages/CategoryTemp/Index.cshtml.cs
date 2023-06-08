using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ShoppyApp.Data;
using ShoppyApp.Model;

namespace ShoppyApp.Pages.CategoryTemp
{
    public class IndexModel : PageModel
    {
        private readonly ShoppyApp.Data.ApplicationDBContext _context;

        public IndexModel(ShoppyApp.Data.ApplicationDBContext context)
        {
            _context = context;
        }

        public IList<Category> Category { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Category != null)
            {
                Category = await _context.Category.ToListAsync();
            }
        }
    }
}
