using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shoppy.DataAccess.Data;
using Shoppy.DataAccess.Repository.IRepository;
using Shoppy.Models;


namespace ShoppyApp.Pages.Admin.Foods

{
    [BindProperties]
    public class DeleteModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public Food Food { get; set; }

        public DeleteModel(IUnitOfWork unitOfWork)
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
                var foodFromDb = _unitOfWork.Food.GetFirstOrDefault(u => u.Id == Food.Id);
                if(foodFromDb != null)
                {
                    _unitOfWork.Food.Remove(foodFromDb);
                    _unitOfWork.Save();
                    TempData["success"] = "Food Category deleted successfully!";
                    return RedirectToPage("Index");
                }
            TempData["error"] = "Error deleting data";
            return Page();
        }
    }
}
