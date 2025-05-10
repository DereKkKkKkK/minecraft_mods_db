using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Entities;

public class Collection
{
    public Guid Id { get; set; }
    public string Name { get; set; } = "";
    public int TimeToComplete { get; set; }
}

public class CollectionMap
{
    public CollectionMap(EntityTypeBuilder<Collection> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).IsRequired();
        builder.Property(x => x.TimeToComplete).IsRequired();
    }
}