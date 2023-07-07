using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shoppy.DataAccess.Repository.IRepository;
using Shoppy.Models;
using Shoppy.Utility;
using Stripe.Checkout;
using System.Security.Claims;

namespace ShoppyApp.Pages.Customer.Cart
{
	[Authorize]
	[BindProperties]
    public class SummaryModel : PageModel
    {
		public IEnumerable<ShoppingCart> ShoppingCartList { get; set; }
		public OrderHeader OrderHeader { get; set; }
		private readonly IUnitOfWork _unitOfWork;
		public SummaryModel(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
			OrderHeader = new OrderHeader();
		}


		public void OnGet()
		{
			var claimsIdentity = (ClaimsIdentity)User.Identity;
			var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
			if (claim != null)
			{
				ShoppingCartList = _unitOfWork.ShoppingCart.GetAll(filter: u => u.ApplicationUserId == claim.Value,
					includeProperties: "MenuItem,MenuItem.Food,MenuItem.Category");
				foreach (var cart in ShoppingCartList)
				{
					OrderHeader.OrderTotal += (cart.MenuItem.Price * cart.Count);
				}
				ApplicationUser applicationUser = _unitOfWork.ApplicationUser.GetFirstOrDefault(u => u.Id == claim.Value);
				OrderHeader.PickupName = applicationUser.FirstName + " " + applicationUser.LastName;
				OrderHeader.PhoneNumber = applicationUser.PhoneNumber;
			}
		}

		public IActionResult OnPost()
		{
			var claimsIdentity = (ClaimsIdentity)User.Identity;
			var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
			if (claim != null)
			{
				ShoppingCartList = _unitOfWork.ShoppingCart.GetAll(filter: u => u.ApplicationUserId == claim.Value,
					includeProperties: "MenuItem,MenuItem.Food,MenuItem.Category");
				foreach (var cart in ShoppingCartList)
				{
					OrderHeader.OrderTotal += (cart.MenuItem.Price * cart.Count);
				}

				OrderHeader.Status = SD.StatusPending;
				OrderHeader.OrderDate = DateTime.Now;
				OrderHeader.UserId = claim.Value;
				OrderHeader.PickUpTime = Convert.ToDateTime(OrderHeader.PickUpDate.ToShortDateString() + " " +
					OrderHeader.PickUpTime.ToShortTimeString());
				_unitOfWork.OrderHeader.Add(OrderHeader);
				_unitOfWork.Save();

				foreach (var item in ShoppingCartList)
				{
					OrderDetails orderDetails = new()
					{
						MenuItemId = item.MenuItemId,
						OrderId = OrderHeader.Id,
						Name = item.MenuItem.Name,
						Price = item.MenuItem.Price,
						Count = item.Count
					};
					_unitOfWork.OrderDetails.Add(orderDetails);
				}
				int quantity = ShoppingCartList.ToList().Count;
				_unitOfWork.ShoppingCart.RemoveRange(ShoppingCartList);
				_unitOfWork.Save();

				var domain = "https://localhost:4242";
				var options = new SessionCreateOptions
				{
					LineItems = new List<SessionLineItemOptions>(),

					Mode = "payment",
					SuccessUrl = domain + $"/Customer/Cart/OrderConfirmation?id={OrderHeader.Id}.html",
					CancelUrl = domain + "/Customer/Cart/Index.html",
				};

				//Add line items
				foreach (var item in ShoppingCartList) { 
					var sessionLineItem = new SessionLineItemOptions
					{
						PriceData = new SessionLineItemPriceDataOptions
						{
							UnitAmount = (long)(item.MenuItem.Price * 100),
							Currency = "usd",
							ProductData = new SessionLineItemPriceDataProductDataOptions
							{
								Name = item.MenuItem.Name,
							},
						},
						Quantity = item.Count
					};
					options.LineItems.Add(sessionLineItem);
				}

                var service = new SessionService();
                Session session = service.Create(options);
                Response.Headers.Add("Location", session.Url);

				OrderHeader.SessionId = session.Id;
				OrderHeader.PaymentIntentId = session.PaymentIntentId;
                return new StatusCodeResult(303);

            }
			return Page();
		}
	}
}
