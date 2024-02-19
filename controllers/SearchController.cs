using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.controllers
{
    public class SearchController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly CategoriesRepository _categoriesRepository;

        public SearchController(ApplicationDbContext db)
        {
            _db = db;
            _categoriesRepository = new CategoriesRepository(_db);
        }
        public IActionResult Index(string search)
        {
            var categories = _categoriesRepository.GetAllCategories();
            return View();
        }
    }
}
