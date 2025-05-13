using DAL.Entities;
namespace DAL.Interfaces;

public interface IRepository<T, in TC, in TU>
{
    Task<List<T>> GetAll();
    Task<PaginatedResult<T>> GetByPage(int pageNumber, int pageSize);
    Task<T> GetById(Guid id);
    Task<T> Create(TC entity);
    Task<T> Update(TU entity);
    Task Delete(Guid id);
}


public class PaginatedResult<T>
{
    public List<T> Items { get; set; }
    public int TotalCount { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
}