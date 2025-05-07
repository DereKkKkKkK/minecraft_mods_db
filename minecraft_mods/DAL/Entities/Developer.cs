using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Entities;

public class Developer
{
    public Guid Id { get; set; }
    public string Nickname { get; set; } = "";
}


public class DeveloperMap
{
    public DeveloperMap(EntityTypeBuilder<Developer> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Nickname).IsRequired();
    }
}