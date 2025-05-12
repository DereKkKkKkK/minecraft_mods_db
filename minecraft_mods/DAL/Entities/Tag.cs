using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Entities;

public class Tag : BaseEntity
{
    public Guid Id { get; set; }
    public string Title { get; set; } = "";
    public List<Mod> Mods { get; set; } = new();
}


public class TagMap
{
    public TagMap(EntityTypeBuilder<Tag> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Title).IsRequired();
    }
}