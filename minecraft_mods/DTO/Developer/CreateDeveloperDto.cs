namespace DTO.Developer;

public class CreateDeveloperDto
{
    public string Nickname { get; set; } = "";
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}