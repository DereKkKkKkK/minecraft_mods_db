using BLL.Interfaces;
using DAL.Interfaces;
using DTO.Book;

namespace BLL.Services;

public class BookService(IRepository<BookDto, CreateBookDto, UpdateBookDto> repository) : IService<BookDto, CreateBookDto, UpdateBookDto>
{
    public async Task<List<BookDto>> GetAll() => await repository.GetAll();
    public async Task<BookDto> GetById(Guid id) => await repository.GetById(id);
    public async Task<BookDto> Create(CreateBookDto book) => await repository.Create(book);
    public async Task<BookDto> Update(UpdateBookDto book) => await repository.Update(book);
    public async Task Delete(Guid id) => await repository.Delete(id);
}