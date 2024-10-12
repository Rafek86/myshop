using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using myshop.Entities.Repositories;
using myshop.Entities.ViewModels;
using System.Security.Claims;

namespace myshop.Web.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class CartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ShoppingCartVm shoppingCartVm { get; set; }

    
        
        public CartController(IUnitOfWork unitOfWork) 
        { 
        _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
           var claimIdentity =(ClaimsIdentity)User.Identity;
           var claim = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);

            shoppingCartVm = new ShoppingCartVm()
            {
                CartsList = _unitOfWork.ShoppingCart.GetAll(x => x.ApplicationUserId == claim.Value,IncludeWord: "Product")
            };
            foreach (var item in shoppingCartVm.CartsList) {
                shoppingCartVm.TotalCarts += (item.Count * item.Product.Price);
            }

            return View(shoppingCartVm);
        }

        public IActionResult Plus(int cartid) { 
        var shoppingcart =_unitOfWork.ShoppingCart.GetFirstOrDefault(x=>x.Id==cartid);
            _unitOfWork.ShoppingCart.IncreaseCount(shoppingcart, 1);
            _unitOfWork.Complete();
            return RedirectToAction("Index");   
        }

        public IActionResult Minus(int cartid) { 
        var shoppingcart =_unitOfWork.ShoppingCart.GetFirstOrDefault(x=>x.Id==cartid);

            if (shoppingcart.Count <= 1) {
                _unitOfWork.ShoppingCart.Remove(shoppingcart);
				_unitOfWork.Complete();
				return RedirectToAction("Index","Home");
            }
            else
            {
				_unitOfWork.ShoppingCart.DecreaseCount(shoppingcart, 1);
			}
			_unitOfWork.Complete();
            return RedirectToAction("Index");   
        }

		public IActionResult remove(int cartid)
		{
			var shoppingcart = _unitOfWork.ShoppingCart.GetFirstOrDefault(x => x.Id == cartid);
            _unitOfWork.ShoppingCart.Remove(shoppingcart);
			_unitOfWork.Complete();
			return RedirectToAction("Index");
		}
	}
}
