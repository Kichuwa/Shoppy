using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoppyApp.Data;
using ShoppyApp.Model;

namespace ShoppyApp.Pages.Categories
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDBContext _db;
        [BindProperty]
        public Category Category { get; set; }

        public EditModel(ApplicationDBContext db)
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

            // Custom Validation for ensuring input is nonduplicate
            if(Category.Name == Category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Category.Name", "The DisplayOrder cannot exactly match the name.");
            }
            if (ModelState.IsValid)
            {
                _db.Category.Update(Category);
                await _db.SaveChangesAsync();
                TempData["success"] = "Category updated successfully!";
                return RedirectToPage("Index");
            }
            TempData["error"] = "Error updating data";
            //Keep user on Page if invalid state
            return Page();
        }
    }
}
