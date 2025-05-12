using DAL.Entities;
namespace DAL.Interfaces;

public interface IRepository<T, in TC, in TU>
{
    Task<List<T>> GetAll();
    Task<T> GetById(Guid id);
    Task<T> Create(TC entity);
    Task<T> Update(TU entity);
    Task Delete(Guid id);
}