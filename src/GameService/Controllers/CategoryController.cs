using GameService.DTOs;
using GameService.Repositories.ForCategory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameService.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoryController(ICategoryRepository categoryRepository) : ControllerBase
{
    private readonly ICategoryRepository _categoryRepository = categoryRepository;


    [HttpPost]
    public async Task<ActionResult> CreateCategory(CategoryDTO model) => Ok(await _categoryRepository.CreateCategory(model));


    [HttpDelete("{categoryId}")]
    public async Task<ActionResult> RemoveCategory([FromRoute] Guid categoryId) 
    => Ok(await _categoryRepository.RemoveCategory(categoryId));


    [HttpPut("{categoryId}")]
    public async Task<ActionResult> UpdateCategory(CategoryDTO model, Guid categoryId) 
    => Ok(await _categoryRepository.UpdateCategory(model, categoryId));


    [HttpGet]
    [Authorize]
    public async Task<ActionResult> GetAllCategories() => Ok(await _categoryRepository.GetAllCategories());
}