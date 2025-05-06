using BLL.Interfaces;
using DAL.Interfaces;
using DTO.Mod;
using DTO.Tag;
namespace BLL.Services;


public class TagService(IRepository<TagDto, CreateTagDto, UpdateTagDto> repository) : IService<TagDto, CreateTagDto, UpdateTagDto>
{
    public async Task<List<TagDto>> GetAll() => await repository.GetAll();
    public async Task<TagDto> GetById(Guid id) => await repository.GetById(id);
    public async Task<TagDto> Create(CreateTagDto book) => await repository.Create(book);
    public async Task<TagDto> Update(UpdateTagDto book) => await repository.Update(book);
    public async Task Delete(Guid id) => await repository.Delete(id);
}