using BLL.Interfaces;
using DAL.Interfaces;
using DTO.ModVersion;
using DTO.Shared;

namespace BLL.Services;

public class VersionService(IRepository<ModVersionDto, CreateModVersionDto, UpdateModVersionDto> repository) : IService<ModVersionDto, CreateModVersionDto, UpdateModVersionDto>
{
    public async Task<List<ModVersionDto>> GetAll() => await repository.GetAll();
    public async Task<QueryParamsDto<ModVersionDto>> GetByPage(int pageNumber, int pageSize) => await repository.GetByPage(pageNumber, pageSize);
    public async Task<ModVersionDto> GetById(Guid id) => await repository.GetById(id);
    public async Task<ModVersionDto> Create(CreateModVersionDto modVersion) => await repository.Create(modVersion);
    public async Task<ModVersionDto> Update(UpdateModVersionDto modVersion) => await repository.Update(modVersion);
    public async Task Delete(Guid id) => await repository.Delete(id);
}