#nullable enable

namespace Documenso.Models;

public partial class TeamProfile
{
    public string Id { get; set; } = null!;

    public bool Enabled { get; set; } = false;

    public int TeamId { get; set; }

    public string? Bio { get; set; }

    public virtual Team? Team { get; set; }
}
