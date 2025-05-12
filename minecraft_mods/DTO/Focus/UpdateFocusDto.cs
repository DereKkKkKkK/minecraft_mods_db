namespace DTO.Focus;

public class UpdateFocusDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public List<Guid> FocusesIds { get; set; } = new();
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}