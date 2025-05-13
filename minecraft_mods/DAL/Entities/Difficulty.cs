using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Entities;


public class Difficulty : BaseEntity
{
    public string Title { get; set; } = "";
    public List<Collection> Collections { get; set; } = new();
}


public class DifficultyMap
{
    public DifficultyMap(EntityTypeBuilder<Difficulty> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Title).IsRequired();
    }
}