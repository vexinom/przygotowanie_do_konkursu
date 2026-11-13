using Backend.DTOs;
using Backend.Models;

namespace Backend.Services;

public class CategoryService : ICategoryService
{
    public Task<IEnumerable<CategoryDto>> GetAll()
    {
        throw new NotImplementedException();
    } 

    public Task<CategoryDto> GetById(Guid id)
    {
        throw new NotImplementedException();
    } 

    public Task<CategoryDto> Create(CreateCategoryDto dto)
    {
        throw new NotImplementedException();
    } 
    public Task<CategoryDto> Update(Guid id, UpdateCategoryDto dto)
    {
        throw new NotImplementedException();
    } 
    public Task Delete(Guid id)
    {
        throw new NotImplementedException();
    } 
}