using DAL.Entities;
using DTO.Shared;

namespace DAL.Interfaces;

public interface IRepository<T, in TC, in TU>
{
    Task<List<T>> GetAll();
    Task<QueryParamsDto<T>> GetByPage(int pageNumber, int pageSize);
    Task<T> GetById(Guid id);
    Task<T> Create(TC entity);
    Task<T> Update(TU entity);
    Task Delete(Guid id);
}