using DTO.Difficulty;
using DTO.Focus;
using DTO.Mod;
using DTO.ModLoader;
using DTO.ModVersion;

namespace DTO.Collection;

public class CollectionDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = "";
    public int TimeToComplete { get; set; }
    public List<ModDto> Mods { get; set; } = new();
    public List<FocusDto> Focuses { get; set; } = new();
    public ModVersionDto Version { get; set; }
    public ModLoaderDto ModLoader { get; set; }
    public DifficultyDto Difficulty { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
    
