using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using myshop.Entities.Models;
using myshop.Entities.Repositories;
using myshop.Entities.ViewModels;

namespace myshop.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnviroment;

        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnviroment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnviroment = webHostEnviroment;
        }

        public IActionResult Index()
        {
            var products = _unitOfWork.Product.GetAll();
            return View(products);
        }

        public IActionResult GetData() { 
            var products = _unitOfWork.Product.GetAll(IncludeWord: "Category");
            return Json( new {data =products});
        }

        [HttpGet]
        public IActionResult Create() {
            ProductVm productVm = new ProductVm()
            {
             Product = new Product(),
             CategoryList = _unitOfWork.Category
            .GetAll()
            .Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            })
            };
            return View(productVm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProductVm productvm,IFormFile file) {
            if (ModelState.IsValid) {

                string RootPath = _webHostEnviroment.WebRootPath;
                if (file != null) { 
                string filename =Guid.NewGuid().ToString();
                    var Upload =Path.Combine(RootPath, @"Images\Products");   
                    var extension =Path.GetExtension(file.FileName);
                    using (var filestream = new FileStream(Path.Combine(Upload, filename + extension), FileMode.Create))
                    {
                        file.CopyTo(filestream);
                    }
                    productvm.Product.Img = @"Images\Products\" + filename + extension;
                   }
                _unitOfWork.Product.Add(productvm.Product);
                _unitOfWork.Complete();
                TempData["Create"] = "Product is Added Successfully";
                return RedirectToAction("Index");   
            }
            return View(productvm);
        }

        [HttpGet]
        public IActionResult Edit(int id) { 

            
            ProductVm productVm = new ProductVm()
            {
                Product = _unitOfWork.Product.GetFirstOrDefault(x => x.Id == id),
                CategoryList = _unitOfWork.Category
                .GetAll()
                .Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                })
            };
            return View(productVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ProductVm productvm,IFormFile? file) {
            if (ModelState.IsValid) {

                string RootPath = _webHostEnviroment.WebRootPath;
                if (file != null)
                {
                    string filename = Guid.NewGuid().ToString();
                    var Upload = Path.Combine(RootPath, @"Images\Products");
                    var extension = Path.GetExtension(file.FileName);
                    
                    if(productvm.Product.Img != null)
                    {
                        var oldImg = Path.Combine(RootPath, productvm.Product.Img.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImg)) { 
                        System.IO.File.Delete(oldImg);
                        }
                    }
                    
                    using (var filestream = new FileStream(Path.Combine(Upload, filename + extension), FileMode.Create))
                    {
                        file.CopyTo(filestream);
                    }
                    productvm.Product.Img = @"Images\Products\" + filename + extension;
                }

                _unitOfWork.Product.Update(productvm.Product);    
                _unitOfWork.Complete();
                TempData["Update"] = "Item Has Updated Successfully";
                return RedirectToAction("Index");
            }
            return View(productvm);
        }

        
        [HttpDelete]
        public IActionResult Delete(int? id) {
        
            var product =_unitOfWork.Product.GetFirstOrDefault(x=>x.Id==id);
            if (product==null)
            {
                Json(new { success = false, message = "Error while Deleting " });
            }
            _unitOfWork.Product.Remove(product);
            var oldImg = Path.Combine(_webHostEnviroment.WebRootPath, product.Img.TrimStart('\\'));
            if (System.IO.File.Exists(oldImg))
            {
                System.IO.File.Delete(oldImg);
            }
            _unitOfWork.Complete();
            Json(new { success = true, message = "File Has been Deleted successfully " });

            TempData["Delete"] = "Item Has Deleted Successfully";
            return RedirectToAction("Index");
        }

    }
}
