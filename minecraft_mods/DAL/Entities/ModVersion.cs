using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Entities;

public class ModVersion
{
    public Guid Id { get; set; }
    public string Title { get; set; } = "";
}


public class VersionMap
{
    public VersionMap(EntityTypeBuilder<ModVersion> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Title).IsRequired();
    }
}