using Microsoft.AspNetCore.Mvc;
using myshop.Entities.Models;
using myshop.DataAccess.Data;
using myshop.Entities.Repositories;
namespace myshop.Web.Areas.Admin.Controllers;

[Area("Admin")]
public class CategoryController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

    public CategoryController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }


    public IActionResult Index()
    {
        var category = _unitOfWork.Category.GetAll();
        return View(category);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Category category)
    {
        if (ModelState.IsValid)
        {
            _unitOfWork.Category.Add(category);
            _unitOfWork.Complete();
            TempData["Create"] = "Item Has Added Successfully";
            return RedirectToAction("Index");
        }
        return View(category);
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        // Fetch the category by ID and pass it to the view
        var category = _unitOfWork.Category.GetFirstOrDefault(x => x.Id == id);
        if (category == null)
        {
            return NotFound();
        }
        return View(category);
    }

    [HttpPost]
    public IActionResult Edit(Category category)
    {
        if (ModelState.IsValid)
        {
            _unitOfWork.Category.Update(category);
            _unitOfWork.Complete();
            TempData["Update"] = "Item Has Updated Successfully";

            return RedirectToAction("Index");
        }
        return View(category);
    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
        // Fetch the category by ID and pass it to the view
        var category = _unitOfWork.Category.GetFirstOrDefault();
        if (category == null)
        {
            return NotFound();
        }
        return View(category);
    }

    [HttpPost]
    public IActionResult Delete(Category category)
    {
        _unitOfWork.Category.Remove(category);
        _unitOfWork.Complete();
        TempData["Delete"] = "Item Has Deleted Successfully";
        return RedirectToAction("Index");
    }
}