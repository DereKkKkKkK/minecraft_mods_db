namespace DTO.Mod;

public class CreateModDto
{
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
    public List<Guid> VersionIds { get; set; } = new();
    public List<Guid> ModLoaderIds { get; set; } = new();
    public bool IsClientside { get; set; }
    public int Downloads { get; set; }
    public double Size { get; set; }
}