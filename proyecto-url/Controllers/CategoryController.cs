using Microsoft.AspNetCore.Mvc;
using proyecto_url.Data.Interfaces;
using proyecto_url.Entities;
using proyecto_url.Models;

namespace proyecto_url.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("{categoryId}")]
        public ActionResult<Category> GetCategoryById(int categoryId)
        {
            var category = _categoryService.GetCategoryById(categoryId);
            if (category == null)
            {
                return NotFound(); // Devuelve 404 si no se encuentra la categoría
            }

            return Ok(category);
        }

        [HttpGet]
        public ActionResult<List<Category>> GetAllCategories()
        {
            var categories = _categoryService.GetAllCategories();
            return Ok(categories);
        }

        [HttpPost]
        public ActionResult<Category> CreateCategory([FromBody] CreateAndUpdateCategoryDTO dto)
        {
            _categoryService.Create(dto);
            return Ok(dto); // Devuelve el DTO creado o podrías devolver la categoría creada
        }

        [HttpPut("{categoryId}")]
        public IActionResult UpdateCategory(int categoryId, [FromBody] CreateAndUpdateCategoryDTO dto)
        {
            var existingCategory = _categoryService.GetCategoryById(categoryId);
            if (existingCategory == null)
            {
                return NotFound(); // Devuelve 404 si no se encuentra la categoría
            }

            _categoryService.Update(dto, categoryId);
            return NoContent(); // Devuelve 204 (sin contenido) para indicar éxito sin datos
        }

        [HttpDelete("{categoryId}")]
        public IActionResult DeleteCategory(int categoryId)
        {
            var existingCategory = _categoryService.GetCategoryById(categoryId);
            if (existingCategory == null)
            {
                return NotFound(); // Devuelve 404 si no se encuentra la categoría
            }

            _categoryService.DeleteCategory(categoryId);
            return NoContent(); // Devuelve 204 (sin contenido) para indicar éxito sin datos
        }
    }
}