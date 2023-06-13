using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shoppy.DataAccess.Data;
using Shoppy.DataAccess.Repository.IRepository;
using Shoppy.Models;

namespace ShoppyApp.Pages.Admin.Categories
{
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        public IEnumerable<Category> Categories { get; set; }

        public IndexModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void OnGet()
        {
            Categories = _unitOfWork.Category.GetAll();
        }
    }
}
