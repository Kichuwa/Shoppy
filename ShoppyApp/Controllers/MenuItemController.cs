using Microsoft.AspNetCore.Mvc;
using Shoppy.DataAccess.Repository.IRepository;

namespace ShoppyApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuItemController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public MenuItemController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var menuItemList = _unitOfWork.MenuItem.GetAll();
            return Json(new { data = menuItemList });
        }
    }
}
