using DAL.Interfaces;

namespace BLL.Interfaces;


public interface IService<T, in TC, in TU>
{
    Task<List<T>> GetAll();
    Task<PaginatedResult<T>> GetByPage(int pageNumber, int pageSize);
    Task<T> GetById(Guid id);
    Task<T> Create(TC entity);
    Task<T> Update(TU entity);
    Task Delete(Guid id);
}