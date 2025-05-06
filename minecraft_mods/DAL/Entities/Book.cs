using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Entities;

public class Book
{
    public Guid Id { get; set; }
    public string Title { get; set; } = "";
    public string Author { get; set; } = "";
    public string Genre { get; set; } = "";
    public DateTime PublishDate { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

public class BookMap
{
    public BookMap(EntityTypeBuilder<Book> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.Title).IsRequired();
        builder.Property(b => b.Author).IsRequired();
        builder.Property(b => b.Genre).IsRequired();
        builder.Property(b => b.PublishDate).IsRequired();
        builder.Property(b => b.CreatedAt).IsRequired();
        builder.Property(b => b.UpdatedAt).IsRequired();
    }
}
