namespace DTO.Difficulty;

public class UpdateDifficultyDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = "";
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}