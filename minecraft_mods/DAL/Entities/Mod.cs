using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Entities;

public class Mod
{
    public Guid Id { get; set; }
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
    public bool IsClientside { get; set; }
    public int Downloads { get; set; }
    public double Size { get; set; }
}


public class ModMap
{
    public ModMap(EntityTypeBuilder<Mod> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Title).IsRequired();
        builder.Property(x => x.Description).IsRequired();
        builder.Property(x => x.IsClientside).IsRequired();
        builder.Property(x => x.Downloads).IsRequired();
        builder.Property(x => x.Size).IsRequired();
    }
}