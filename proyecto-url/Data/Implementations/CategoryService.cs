using proyecto_url;
using proyecto_url.Entities;
using System;
using proyecto_url.Data.Interfaces;
using proyecto_url.Models;

namespace proyecto_url.Data.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly UrlShorterContext _context;

        public CategoryService(UrlShorterContext context)
        {
            _context = context;
        }

        public Category GetCategoryById(int categoryId)
        {
            return _context.Categories.Find(categoryId);
        }

        public List<Category> GetAllCategories()
        {
            return _context.Categories.ToList();
        }

        public Category Create(CreateAndUpdateCategoryDTO dto)
        {
            Category newCategory = new Category()
            {
                Name = dto.Name,
            };
            _context.Categories.Add(newCategory);
            _context.SaveChanges();

            return newCategory;
        }

        public void Update(CreateAndUpdateCategoryDTO dto, int CategoryId)
        {
            Category categoryToUpdate = _context.Categories.First(u => u.Id == CategoryId);
            categoryToUpdate.Name = dto.Name;
            _context.SaveChanges();
        }

        public void DeleteCategory(int categoryId)
        {
            var category = _context.Categories.Find(categoryId);
            if (category != null)
            {
                _context.Categories.Remove(category);
                _context.SaveChanges();
            }
        }
    }
}





