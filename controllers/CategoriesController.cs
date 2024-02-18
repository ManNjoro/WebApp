using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.controllers
{
    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly CategoriesRepository _categoriesRepository;

        public CategoriesController(ApplicationDbContext db)
        {
            _db = db;
            _categoriesRepository = new CategoriesRepository(_db);
        }
        public IActionResult Index()
        {
            var categories = _categoriesRepository.GetAllCategories();
            return View(categories);
        }

        public IActionResult Edit(string id)
        {
            ViewBag.Action = "edit";
            var category = _categoriesRepository.GetCategoryById(id);
            return View(category);
        } 

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _categoriesRepository.UpdateCategory(category.CategoryId, category);
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            return View();
        }

        [HttpPost]
        public IActionResult Add(Category category)
        {
            if (ModelState.IsValid)
            {
                _categoriesRepository.AddCategory(category);
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        
        public IActionResult Delete(string categoryId)
        {
            _categoriesRepository.DeleteCategory(categoryId);
            return RedirectToAction(nameof(Index));
        }
    }
}
