using GameService.DTOs;
using GameService.Entities.Base;

namespace GameService.Repositories.ForCategory;

public interface ICategoryRepository
{
    Task<BaseResponseModel> CreateCategory(CategoryDTO category);
    Task<bool> RemoveCategory(Guid categoryId);
    Task<BaseResponseModel> UpdateCategory(CategoryDTO category,Guid categoryId);
    Task<BaseResponseModel> GetAllCategories();
}