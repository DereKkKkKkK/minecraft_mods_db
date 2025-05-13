using BLL.Interfaces;
using DAL.Interfaces;
using DTO.Developer;

namespace BLL.Services;

public class DeveloperService(IRepository<DeveloperDto, CreateDeveloperDto, UpdateDeveloperDto> repository) : IService<DeveloperDto, CreateDeveloperDto, UpdateDeveloperDto>
{
    public async Task<List<DeveloperDto>> GetAll() => await repository.GetAll();
    public async Task<PaginatedResult<DeveloperDto>> GetByPage(int pageNumber, int pageSize) => await repository.GetByPage(pageNumber, pageSize);
    public async Task<DeveloperDto> GetById(Guid id) => await repository.GetById(id);
    public async Task<DeveloperDto> Create(CreateDeveloperDto developer) => await repository.Create(developer);
    public async Task<DeveloperDto> Update(UpdateDeveloperDto developer) => await repository.Update(developer);
    public async Task Delete(Guid id) => await repository.Delete(id);
}