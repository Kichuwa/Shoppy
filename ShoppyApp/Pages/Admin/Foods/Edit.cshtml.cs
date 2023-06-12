using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shoppy.DataAccess.Data;
using Shoppy.Models;

namespace ShoppyApp.Pages.Admin.Foods
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDBContext _db;
        [BindProperty]
        public Food Food { get; set; }

        public EditModel(ApplicationDBContext db)
        {
            _db = db;
        }
        //Find primary key in DB and return item.
        public void OnGet(int id)
        {
            Food = _db.Food.Find(id);
        }

        public async Task<IActionResult> OnPost()
        {
            // Custom Validation for ensuring input is nonduplicate
            if (ModelState.IsValid)
            {
                _db.Food.Update(Food);
                await _db.SaveChangesAsync();
                TempData["success"] = "Food Category updated successfully!";
                return RedirectToPage("Index");
            }
            TempData["error"] = "Error updating data";
            //Keep user on Page if invalid state
            return Page();
        }
    }
}
