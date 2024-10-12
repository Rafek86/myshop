using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using myshop.Entities.Models;
using myshop.Entities.Repositories;
using System.Security.Claims;

namespace myshop.Web.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var products = _unitOfWork.Product.GetAll();
            return View(products);
        }

        public IActionResult Details(int ProductId)
        {
            ShoppingCart obj = new ShoppingCart()
            {
                ProductId = ProductId,
                Product = _unitOfWork.Product.GetFirstOrDefault(x => x.Id == ProductId, IncludeWord: "Category"),
                Count = 1
            };
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Details(ShoppingCart shoppingcart)
        {
            var claimIdentity =(ClaimsIdentity)User.Identity;
            var claim = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);
            shoppingcart.ApplicationUserId = claim.Value;

            ShoppingCart Cartobj = _unitOfWork.ShoppingCart.
                GetFirstOrDefault(x => x.ApplicationUserId == claim.Value && x.ProductId == shoppingcart.ProductId);

            if (Cartobj == null)
            {
                _unitOfWork.ShoppingCart.Add(shoppingcart);
            }
            else {
              _unitOfWork.ShoppingCart.IncreaseCount(Cartobj,shoppingcart.Count);   
            }

            _unitOfWork.Complete();
            return RedirectToAction("Index");   
        }
    }
}
