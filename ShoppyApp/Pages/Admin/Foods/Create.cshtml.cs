using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shoppy.DataAccess.Data;
using Shoppy.Models;

namespace ShoppyApp.Pages.Admin.Foods
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDBContext _db;
        [BindProperty]
        public Food Food { get; set; }

        public CreateModel(ApplicationDBContext db)
        {
            _db = db;
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                await _db.Food.AddAsync(Food);
                await _db.SaveChangesAsync();
                TempData["success"] = "Food Category created successfully!";
                return RedirectToPage("Index");
            }
            //Keep user on Page if invalid state
            TempData["error"] = "Error creating data";
            return Page();
        }
    }
}
