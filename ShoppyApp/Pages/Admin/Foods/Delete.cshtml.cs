using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shoppy.DataAccess.Data;
using Shoppy.Models;

namespace ShoppyApp.Pages.Admin.Foods
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDBContext _db;
        [BindProperty]
        public Food Food { get; set; }

        public DeleteModel(ApplicationDBContext db)
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
                var foodFromDb = _db.Category.Find(Food.Id);
                if(foodFromDb != null)
                {
                    _db.Category.Remove(foodFromDb);
                    await _db.SaveChangesAsync();
                    TempData["success"] = "Food Category deleted successfully!";
                    return RedirectToPage("Index");
                }
            TempData["error"] = "Error deleting data";
            return Page();
        }
    }
}
