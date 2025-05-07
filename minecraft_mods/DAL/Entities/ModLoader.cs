using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Entities;

public class ModLoader
{
    public Guid Id { get; set; }
    public string Title { get; set; } = "";
}


public class ModLoaderMap
{
    public ModLoaderMap(EntityTypeBuilder<ModLoader> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Title).IsRequired();
    }
}