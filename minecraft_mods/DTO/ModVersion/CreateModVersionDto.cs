namespace DTO.ModVersion;

public class CreateModVersionDto
{
    public string Title { get; set; } = "";
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}