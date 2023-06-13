using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shoppy.DataAccess.Data;
using Shoppy.DataAccess.Repository.IRepository;
using Shoppy.Models;

namespace ShoppyApp.Pages.Admin.Categories
{
    [BindProperties]
    public class EditModel : PageModel
    {

        private readonly IUnitOfWork _unitOfWork;
        public Category Category { get; set; }
        public EditModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        

        //Find primary key in DB and return item.
        public void OnGet(int id)
        {
            Category = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
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
                _unitOfWork.Category.Update(Category);
                _unitOfWork.Save();
                TempData["success"] = "Category updated successfully!";
                return RedirectToPage("Index");
            }
            TempData["error"] = "Error updating data";
            //Keep user on Page if invalid state
            return Page();
        }
    }
}
