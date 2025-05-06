namespace DTO.Mod;

public class UpdateModDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
    public bool IsClientside { get; set; }
    public int Downloads { get; set; }
    public double Size { get; set; }
}