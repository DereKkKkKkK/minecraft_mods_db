using BLL.Interfaces;
using DAL.Interfaces;
using DTO.ModLoader;

namespace BLL.Services;

public class ModLoaderService(IRepository<ModLoaderDto, CreateModLoaderDto, UpdateModLoaderDto> repository) : IService<ModLoaderDto, CreateModLoaderDto, UpdateModLoaderDto>
{
    public async Task<List<ModLoaderDto>> GetAll() => await repository.GetAll();
    public async Task<ModLoaderDto> GetById(Guid id) => await repository.GetById(id);
    public async Task<ModLoaderDto> Create(CreateModLoaderDto modLoader) => await repository.Create(modLoader);
    public async Task<ModLoaderDto> Update(UpdateModLoaderDto modLoader) => await repository.Update(modLoader);
    public async Task Delete(Guid id) => await repository.Delete(id);
}