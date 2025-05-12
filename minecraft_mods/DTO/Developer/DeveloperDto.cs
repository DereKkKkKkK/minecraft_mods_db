namespace DTO.Developer;

public class DeveloperDto
{
    public Guid Id { get; set; }
    public string Nickname { get; set; } = "";
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}