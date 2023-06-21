using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shoppy.DataAccess.Data;
using Shoppy.DataAccess.Repository.IRepository;
using Shoppy.Models;

namespace ShoppyApp.Pages.Admin.MenuItems
{
    [BindProperties]
    public class UpsertModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        
        public MenuItem MenuItem { get; set; }

        public UpsertModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            MenuItem = new();
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.MenuItem.Add(MenuItem);
                _unitOfWork.Save();
                TempData["success"] = "Food Category created successfully!";
                return RedirectToPage("Index");
            }
            //Keep user on Page if invalid state
            TempData["error"] = "Error creating data";
            return Page();
        }
    }
}
