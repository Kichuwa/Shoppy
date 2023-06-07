using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoppyApp.Data;
using ShoppyApp.Model;

namespace ShoppyApp.Pages.Categories
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDBContext _db;
        [BindProperty]
        public Category Category { get; set; }

        public CreateModel(ApplicationDBContext db)
        {
            _db = db;
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            await _db.Category.AddAsync(Category);
            await _db.SaveChangesAsync();
            return RedirectToPage("Index");
        }
    }
}
