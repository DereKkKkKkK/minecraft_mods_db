using BLL.Interfaces;
using DAL.Interfaces;
using DTO.Focus;
using DTO.Shared;

namespace BLL.Services;

public class FocusService(IRepository<FocusDto, CreateFocusDto, UpdateFocusDto> repository) : IService<FocusDto, CreateFocusDto, UpdateFocusDto>
{
    public async Task<List<FocusDto>> GetAll() => await repository.GetAll();
    public async Task<QueryParamsDto<FocusDto>> GetByPage(int pageNumber, int pageSize) => await repository.GetByPage(pageNumber, pageSize);
    public async Task<FocusDto> GetById(Guid id) => await repository.GetById(id);
    public async Task<FocusDto> Create(CreateFocusDto focus) => await repository.Create(focus);
    public async Task<FocusDto> Update(UpdateFocusDto focus) => await repository.Update(focus);
    public async Task Delete(Guid id) => await repository.Delete(id);
}