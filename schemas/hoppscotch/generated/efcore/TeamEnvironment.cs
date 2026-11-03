#nullable enable

namespace Hoppscotch.Models;

public partial class TeamEnvironment
{
    public string Id { get; set; } = null!;

    public string TeamID { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Variables { get; set; } = null!;

    public virtual Team Team { get; set; } = null!;
}
