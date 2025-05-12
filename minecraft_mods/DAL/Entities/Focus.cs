using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace DAL.Entities;

public class Focus : BaseEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = "";
    public List<Collection> Collections { get; set; } = new();
    
}

public class FocusMap
{
    public FocusMap(EntityTypeBuilder<Focus> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).IsRequired();
    }
}