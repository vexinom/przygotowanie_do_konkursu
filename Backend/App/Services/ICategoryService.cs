using Backend.DTOs;

namespace Backend.Services;

public interface ICategoryService
{
    Task<IEnumerable<CategoryDto>> GetAll();

    Task<CategoryDto> GetById(Guid id);

    Task<CategoryDto> Create(CreateCategoryDto dto);

    Task<CategoryDto> Update(Guid id, UpdateCategoryDto dto);

    Task Delete(Guid id);
}