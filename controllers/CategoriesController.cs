using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using WebApp.Models;
using WebApp.Views.Shared.Components.SearchBar;

namespace WebApp.controllers
{
    [Authorize]
    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly CategoriesRepository _categoriesRepository;

        public CategoriesController(ApplicationDbContext db)
        {
            _db = db;
            _categoriesRepository = new CategoriesRepository(_db);
        }
        private List<SelectListItem> GetPageSizes(int selectedPageSize = 10)
        {
            var pagesSizes = new List<SelectListItem>();
            if (selectedPageSize == 5)
                pagesSizes.Add(new SelectListItem("5", "5", true));
            else
                pagesSizes.Add(new SelectListItem("5", "5"));

            for(int lp = 10; lp <= 100; lp +=10)
            {
                if (lp == selectedPageSize)
                {
                    pagesSizes.Add(new SelectListItem(lp.ToString(), lp.ToString(), true));
                }
                else
                    pagesSizes.Add(new SelectListItem(lp.ToString(), lp.ToString()));
            }
            return pagesSizes;
        }
        public IActionResult Index(int pg=1, string SearchText = "", int pageSize = 5)
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

            if (pg < 1) pg = 1;
            int recsCount = categories.Count();
            var pager = new Pager(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var data = categories.Skip(recSkip).Take(pager.PageSize).ToList();
            SPager SearchPager = new SPager(recsCount, pg, pageSize) { Action = "index", Controller = "categories", SearchText = SearchText };
            ViewBag.SearchPager = SearchPager;
            this.ViewBag.PageSizes = GetPageSizes(pageSize);
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
