namespace DTO.Collection;

public class CreateCollectionDto
{
    public string Name { get; set; } = "";
    public int TimeToComplete { get; set; }
    public List<Guid> ModsIds { get; set; } = new();
    public List<Guid> FocusesIds { get; set; } = new();
    public Guid VersionId { get; set; }
    public Guid ModLoaderId { get; set; }
    public Guid DifficultyId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}