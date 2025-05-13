using DTO.ModLoader;
using DTO.ModVersion;

namespace DTO.Focus;

public class FocusDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}