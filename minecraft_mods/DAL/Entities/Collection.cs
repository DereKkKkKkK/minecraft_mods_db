using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Entities;

public class Collection : BaseEntity

{
    public string Name { get; set; } = "";
    public int TimeToComplete { get; set; }
    public List<Mod> Mods { get; set; } = new();
    public List<Focus> Focuses { get; set; } = new();
    public ModVersion Version { get; set; }
    public ModLoader ModLoader { get; set; }
    public Difficulty Difficulty { get; set; }
}

public class CollectionMap
{
    public CollectionMap(EntityTypeBuilder<Collection> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).IsRequired();
        builder.Property(x => x.TimeToComplete).IsRequired();
        
        
        builder
            .HasMany(c => c.Mods)
            .WithMany(m => m.Collections);
        
        
        builder
            .HasMany(c => c.Focuses)
            .WithMany(m => m.Collections);
        
        
        builder
            .HasOne(c => c.Version)
            .WithMany(v => v.Collections);
        
        
        builder
            .HasOne(c => c.ModLoader)
            .WithMany(l => l.Collections);
        
        
        builder
            .HasOne(c => c.Difficulty)
            .WithMany(d => d.Collections);
    }
}