using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Entities;

public class ModVersion : BaseEntity
{
    public string Title { get; set; } = "";
    public List<Mod> Mods { get; set; } = new();
    public List<Collection> Collections { get; set; } = new();
}


public class VersionMap
{
    public VersionMap(EntityTypeBuilder<ModVersion> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Title).IsRequired();


        builder
            .HasMany(v => v.Mods)
            .WithMany(m => m.Versions);
        
        
        builder
            .HasMany(v => v.Collections)
            .WithOne(c => c.Version);
    }
}