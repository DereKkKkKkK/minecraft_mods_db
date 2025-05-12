using BLL.Interfaces;
using DAL.Interfaces;
using DTO.Mod;

namespace BLL.Services;

public class ModService(IRepository<ModDto, CreateModDto, UpdateModDto> repository) : IService<ModDto, CreateModDto, UpdateModDto>
{
    public async Task<List<ModDto>> GetAll() => await repository.GetAll();
    public async Task<ModDto> GetById(Guid id) => await repository.GetById(id);
    public async Task<ModDto> Create(CreateModDto mod) => await repository.Create(mod);
    public async Task<ModDto> Update(UpdateModDto mod) => await repository.Update(mod);
    public async Task Delete(Guid id) => await repository.Delete(id);
}