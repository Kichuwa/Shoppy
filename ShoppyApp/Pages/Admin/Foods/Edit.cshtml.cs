using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shoppy.DataAccess.Data;
using Shoppy.DataAccess.Repository.IRepository;
using Shoppy.Models;

namespace ShoppyApp.Pages.Admin.Foods
{
    [BindProperties]
    public class EditModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        public Food Food { get; set; }
        public EditModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        //Find primary key in DB and return item.
        public void OnGet(int id)
        {
            Food = _unitOfWork.Food.GetFirstOrDefault(u => u.Id == id);
        }

        public async Task<IActionResult> OnPost()
        {
            // Custom Validation for ensuring input is nonduplicate
            if (ModelState.IsValid)
            {
                _unitOfWork.Food.Update(Food);
                _unitOfWork.Save();
                TempData["success"] = "Food Category updated successfully!";
                return RedirectToPage("Index");
            }
            TempData["error"] = "Error updating data";
            //Keep user on Page if invalid state
            return Page();
        }
    }
}
