namespace DTO.Focus;

public class CreateFocusDto
{
    public string Name { get; set; }
    public List<Guid> FocusesIds { get; set; } = new();
}