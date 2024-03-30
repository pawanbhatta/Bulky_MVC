using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepo;

        public CategoryController(ICategoryRepository context)
        {
            _categoryRepo = context;
        }
        public IActionResult Index()
        {
            List<Category> categories = _categoryRepo.GetAll().ToList();
            return View(categories);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category category)
        {
            //Adding custom error message
            if (category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The display order cannot exactly match the name.");
            }
            if (category.Name != null && category.Name.ToLower() == "test")
            {
                ModelState.AddModelError("", "Test is an invalid value.");
            }
            if (ModelState.IsValid)
            {
                _categoryRepo.Add(category);
                _categoryRepo.Save();
                TempData["success"] = "Category Added Successfully";
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound("Category Not Found");
            }
            //different ways of getting category by id
            //first way
            Category? category = _categoryRepo.Get(u => u.Id == id); //only works with primary key
            //Category? category1 = _context.Categories.FirstOrDefault(u => u.Id == id); //works with any property (here used id, we can also use name or anything)
            //Category? category2 = _context.Categories.Where(u => u.Id == id).FirstOrDefault();
            if (category == null)
            {
                return NotFound("Category Not Found");
            }
            return View(category);
        }
        [HttpPost]
        public IActionResult Edit(Category category)
        {

            if (ModelState.IsValid)
            {
                _categoryRepo.Update(category);
                _categoryRepo.Save();
                TempData["success"] = "Category Updated Successfully";
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound("Category Not Found");
            }
            Category? category = _categoryRepo.Get(u => u.Id == id); //only works with primary key
            if (category == null)
            {
                return NotFound("Category Not Found");
            }
            return View(category);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteCategory(int? id)
        {
            Category? category = _categoryRepo.Get(u => u.Id == id); //only works with primary key
            if (category == null)
            {
                return NotFound("Category Not Found");
            }
            _categoryRepo.Remove(category);
            _categoryRepo.Save();
            TempData["success"] = "Category Deleted Successfully";
            return RedirectToAction("Index");
        }
    }
}
