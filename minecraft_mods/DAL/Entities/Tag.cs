using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Entities;

public class Tag
{
    public Guid Id { get; set; }
    public string Title { get; set; } = "";
}


public class TagMap
{
    public TagMap(EntityTypeBuilder<Tag> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Title).IsRequired();
    }
}