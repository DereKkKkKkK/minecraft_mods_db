using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Entities;

public class ModLoader
{
    public Guid Id { get; set; }
    public string Title { get; set; } = "";
    public List<Mod> Mods { get; set; } = new();
    public List<Collection> Collections { get; set; } = new();
}


public class ModLoaderMap
{
    public ModLoaderMap(EntityTypeBuilder<ModLoader> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Title).IsRequired();
        
        
        builder
            .HasMany(v => v.Mods)
            .WithMany(m => m.ModLoaders);
    }
}