using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using WebApp.Models;
using WebApp.Views.Shared.Components.SearchBar;

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
        public IActionResult Index(int pg=1, string SearchText = "")
        {
            List<Category> categories;
            if (SearchText != "" && SearchText != null)
            {
                categories = _db.Categories
                    .Where(cat => cat.Name.Contains(SearchText))
                    .ToList();
            }
            else
                categories = _categoriesRepository.GetAllCategories();

            const int pageSize = 5;
            if (pg < 1) pg = 1;
            int recsCount = categories.Count();
            var pager = new Pager(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var data = categories.Skip(recSkip).Take(pager.PageSize).ToList();
            SPager SearchPager = new SPager(recsCount, pg, pageSize) { Action = "index", Controller = "categories", SearchText = SearchText };
            ViewBag.SearchPager = SearchPager;
            // this.ViewBag.Pager = pager;
            return View(data);
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
                _categoriesRepository.UpdateCategory(category.CategoryId, category, TempData);
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
                _categoriesRepository.AddCategory(category, TempData);
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        
        public IActionResult Delete(string categoryId)
        {
            _categoriesRepository.DeleteCategory(categoryId, TempData);
            return RedirectToAction(nameof(Index));
        }
    }
}
