using DAL.EF;
using DAL.Entities;
using DAL.Interfaces;
using DTO.Book;
using Microsoft.EntityFrameworkCore;
namespace DAL.Repositories;

public class BookRepository(ApplicationContext context) : IRepository<BookDto, CreateBookDto, UpdateBookDto>
{
    public async Task<List<BookDto>> GetAll()
    {
        List<Book> books = await context.Books.ToListAsync();
        List<BookDto> bookList = new List<BookDto>();

        
        foreach (var book in books)
        {
            BookDto bookDto = new()
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                Genre = book.Genre,
                PublishDate = book.PublishDate,
                CreatedAt = book.CreatedAt,
                UpdatedAt = book.UpdatedAt,
            };
            bookList.Add(bookDto);
        }
        
        return bookList;
    }

    public async Task<BookDto> GetById(Guid id)
    {
        Book? book = await context.Books.FindAsync(id);

        return new BookDto()
        {
            Id = book.Id,
            Title = book.Title,
            Author = book.Author,
            Genre = book.Genre,
            PublishDate = book.PublishDate,
            CreatedAt = book.CreatedAt,
            UpdatedAt = book.UpdatedAt
        };
    }

    public async Task<BookDto> Create(CreateBookDto book)
    {
        Book createdBook = new()
        {
            Title = book.Title,
            Author = book.Author,
            Genre = book.Genre,
            PublishDate = book.PublishDate.ToUniversalTime(),
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        
        context.Books.Add(createdBook);
        await context.SaveChangesAsync();

        return new BookDto()
        {
            Id = createdBook.Id,
            Title = createdBook.Title,
            Author = createdBook.Author,
            Genre = createdBook.Genre,
            PublishDate = createdBook.PublishDate.ToUniversalTime(),
            CreatedAt = createdBook.CreatedAt,
            UpdatedAt = createdBook.UpdatedAt
        };
    }

    public async Task<BookDto> Update(UpdateBookDto book)
    {
        Book? updatedBook = await context.Books.FindAsync(book.Id);
        
        updatedBook.Title = book.Title;
        updatedBook.Author = book.Author;
        updatedBook.Genre = book.Genre;
        updatedBook.PublishDate = book.PublishDate.ToUniversalTime();
        updatedBook.UpdatedAt = DateTime.UtcNow;
        
        context.Books.Update(updatedBook);
        await context.SaveChangesAsync();

        return new BookDto()
        {
            Id = updatedBook.Id,
            Title = updatedBook.Title,
            Author = updatedBook.Author,
            Genre = updatedBook.Genre,
            PublishDate = updatedBook.PublishDate.ToUniversalTime(),
            CreatedAt = updatedBook.CreatedAt,
            UpdatedAt = updatedBook.UpdatedAt
        };
    }

    public async Task Delete(Guid id)
    {
        Book? book = await context.Books.FindAsync(id);
        context.Books.Remove(book);
        await context.SaveChangesAsync();
    }
}