
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
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


        public void AddCategory(Category category, ITempDataDictionary tempData)
        {
            if (category == null)
            {
                throw new ArgumentNullException(nameof(category));
            }

            try
            {
                _db.Categories.Add(category);
                _db.SaveChanges();
                tempData["AlertMessage"] = "Category Created Successfully...";
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                Console.WriteLine($"Error adding category: {ex.Message}");
                throw; // Re-throw the exception to propagate it up the call stack
            }
        }


        public Category? GetCategoryById(string categoryId)
        {
            var category = _db.Categories.FirstOrDefault(x => x.CategoryId == categoryId);
            if (category != null) 
            {
                return category;
            }
            return null;
        }

        public void UpdateCategory(string categoryId, Category category, ITempDataDictionary tempData)
        {
            if (categoryId != category.CategoryId) return;
            var categoryToUpdate = GetCategoryById(categoryId);
            if (categoryToUpdate != null )
            {
                categoryToUpdate.Name = category.Name;
                categoryToUpdate.Description = category.Description;
                _db.SaveChanges();
                tempData["AlertMessage"] = "Category Updated Successfully...";
            }
        }

        public void DeleteCategory(string categoryId, ITempDataDictionary tempData)
        {
            try
            {
                var category = GetCategoryById(categoryId);
                if (category != null)
                {
                    _db.Categories.Remove(category);
                    _db.SaveChanges();
                    tempData["AlertMessage"] = "Category Deleted Successfully...";
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                Console.WriteLine($"Error deleting category: {ex.Message}");
                throw; // Re-throw the exception to propagate it up the call stack
            }
        }

    }
}
