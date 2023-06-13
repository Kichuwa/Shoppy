using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shoppy.DataAccess.Data;
using Shoppy.DataAccess.Repository.IRepository;
using Shoppy.Models;

namespace ShoppyApp.Pages.Admin.Categories
{
    [BindProperties]
    public class CreateModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        public Category Category { get; set; }
        public CreateModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        public void OnGet()
        {
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
                _unitOfWork.Category.Add(Category);
                _unitOfWork.Save();
                TempData["success"] = "Category created successfully!";
                return RedirectToPage("Index");
            }
            //Keep user on Page if invalid state
            TempData["error"] = "Error creating data";
            return Page();
        }
    }
}
