using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Entities;

public class Collection
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int TimeToComplete { get; set; }
}

public class CollectionMap
{
    public CollectionMap(EntityTypeBuilder<Collection> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasKey(x => x.Name);
        builder.HasKey(x => x.TimeToComplete);
    }
}