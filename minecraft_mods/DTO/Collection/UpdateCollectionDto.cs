namespace DTO.Collection;

public class UpdateCollectionDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int TimeToComplete { get; set; }
}