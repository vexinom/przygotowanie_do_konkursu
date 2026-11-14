using Backend.DTOs;
using Backend.Models;

namespace Backend.Services;

public class ContestService : IContestService
{
    public Task<IEnumerable<ContestDto>> GetAll()
    {
        throw new NotImplementedException();
    } 

    public Task<ContestDto> GetById(Guid id)
    {
        throw new NotImplementedException();
    } 

    public Task<ContestDto> Create(CreateContestDto dto)
    {
        throw new NotImplementedException();
    } 
    public Task<ContestDto> Update(Guid id, UpdateContestDto dto)
    {
        throw new NotImplementedException();
    } 
    public Task Delete(Guid id)
    {
        throw new NotImplementedException();
    } 
}