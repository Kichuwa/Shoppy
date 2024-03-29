using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shoppy.DataAccess.Repository.IRepository;
using Shoppy.Models;
using Shoppy.Models.ViewModel;
using Shoppy.Utility;

namespace ShoppyApp.Pages.Admin.Order
{
    [Authorize(Roles = $"{SD.ManagerRole},{SD.KitchenRole}")]
    public class ManageOrderModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        public List<OrderDetailVM> OrderDetailsVM { get; set; }
        public ManageOrderModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void OnGet()
        {
            OrderDetailsVM = new();

            List<OrderHeader> orderHeaders = _unitOfWork.OrderHeader.GetAll(u => u.Status == SD.StatusSubmitted ||  u.Status == SD.StatusInProcess).ToList();

            foreach (OrderHeader item in orderHeaders) 
            {
                OrderDetailVM individual = new OrderDetailVM()
                {
                    OrderHeader = item,
                    OrderDetails = _unitOfWork.OrderDetails.GetAll(u => u.OrderId == item.Id).ToList()
                };
                OrderDetailsVM.Add(individual);
            }
        }

        public IActionResult OnPostOrderInProcess(int orderId) {
            _unitOfWork.OrderHeader.UpdateStatus(orderId, SD.StatusInProcess);
            _unitOfWork.Save();
            return RedirectToPage("ManageOrder");
        }

        public IActionResult OnPostOrderReady(int orderId)
        {
            _unitOfWork.OrderHeader.UpdateStatus(orderId, SD.StatusReady);
            _unitOfWork.Save();
            return RedirectToPage("ManageOrder");
        }

        public IActionResult OnPostOrderCancel(int orderId)
        {
            _unitOfWork.OrderHeader.UpdateStatus(orderId, SD.StatusCancelled);
            _unitOfWork.Save();
            return RedirectToPage("ManageOrder");
        }
    }
}
