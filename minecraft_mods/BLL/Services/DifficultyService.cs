using BLL.Interfaces;
using DAL.Interfaces;
using DTO.Difficulty;
using DTO.Shared;

namespace BLL.Services;

public class DifficultyService(IRepository<DifficultyDto, CreateDifficultyDto, UpdateDifficultyDto> repository) : IService<DifficultyDto, CreateDifficultyDto, UpdateDifficultyDto>
{
    public async Task<List<DifficultyDto>> GetAll() => await repository.GetAll();
    public async Task<QueryParamsDto<DifficultyDto>> GetByPage(QueryParamsDto<DifficultyDto> queryParams) => await repository.GetByPage(queryParams);
    public async Task<DifficultyDto> GetById(Guid id) => await repository.GetById(id);
    public async Task<DifficultyDto> Create(CreateDifficultyDto difficulty) => await repository.Create(difficulty);
    public async Task<DifficultyDto> Update(UpdateDifficultyDto difficulty) => await repository.Update(difficulty);
    public async Task Delete(Guid id) => await repository.Delete(id);
}