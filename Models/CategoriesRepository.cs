
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Models
{
    
    public class CategoriesRepository
    {
        private readonly ApplicationDbContext _db;
        public CategoriesRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        private static List<Category> _categories = new List<Category>();
        public List<Category>? GetAllCategories()
        {
            try
            {
                return _db.Categories.ToList();
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                Console.WriteLine($"Error fetching categories: {ex.Message}");
                return null;
            }
        }


        public void AddCategory(Category category)
        {
            if (category == null)
            {
                throw new ArgumentNullException(nameof(category));
            }

            try
            {
                _db.Categories.Add(category);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                Console.WriteLine($"Error adding category: {ex.Message}");
                throw; // Re-throw the exception to propagate it up the call stack
            }
        }

        public static List<Category> GetCategories() => _categories;

        public static Category? GetCategoryById(string categoryId)
        {
            var category = _categories.FirstOrDefault(x => x.CategoryId == categoryId);
            if (category != null) 
            {
                return new Category
                {
                    CategoryId = category.CategoryId,
                    Name = category.Name,
                    Description = category.Description
                };
            }
            return null;
        }

        public static void UpdateCategory(string categoryId, Category category)
        {
            if (categoryId != category.CategoryId) return;
            var categoryToUpdate = _categories.FirstOrDefault(x => x.CategoryId == categoryId);
            if (categoryToUpdate != null )
            {
                categoryToUpdate.Name = category.Name;
                categoryToUpdate.Description = category.Description;
            }
        }

        public static void DeleteCategory(string categoryId)
        {
            var category = _categories.FirstOrDefault(x => x.CategoryId == categoryId);
            if (category != null)
            {
                _categories.Remove(category);
            }
        }
    }
}
