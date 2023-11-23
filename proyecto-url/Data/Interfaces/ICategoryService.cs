using proyecto_url.Entities;
using proyecto_url.Models;
using System;

namespace proyecto_url.Data.Interfaces;

public interface ICategoryService
{
        Category Create(CreateAndUpdateCategoryDTO dto);
        Category GetCategoryById(int categoryId);
        List<Category> GetAllCategories();
        
        void Update(CreateAndUpdateCategoryDTO dto, int CategoryId);
        void DeleteCategory(int categoryId);
}