using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Entities;

public class Developer : BaseEntity
{
    public string Nickname { get; set; } = "";
    public List<Mod> Mods { get; set; } = new();
}


public class DeveloperMap
{
    public DeveloperMap(EntityTypeBuilder<Developer> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Nickname).IsRequired();
        
        
        builder
            .HasMany(d => d.Mods)
            .WithMany(m => m.Developers);
    }
}