using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shoppy.DataAccess.Data;
using Shoppy.Models;

namespace ShoppyApp.Pages.Admin.Categories
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDBContext _db;
        [BindProperty]
        public Category Category { get; set; }

        public DeleteModel(ApplicationDBContext db)
        {
            _db = db;
        }
        //Find primary key in DB and return item.
        public void OnGet(int id)
        {
            Category = _db.Category.Find(id);
        }

        public async Task<IActionResult> OnPost()
        {
                var categoryFromDb = _db.Category.Find(Category.Id);
                if(categoryFromDb != null)
                {
                    _db.Category.Remove(categoryFromDb);
                    await _db.SaveChangesAsync();
                    TempData["success"] = "Category deleted successfully!";
                    return RedirectToPage("Index");
                }
            TempData["error"] = "Error deleting data";
            return Page();
        }
    }
}
