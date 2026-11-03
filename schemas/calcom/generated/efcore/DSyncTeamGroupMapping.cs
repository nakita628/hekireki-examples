#nullable enable

namespace Calcom.Models;

public partial class DSyncTeamGroupMapping
{
    public int Id { get; set; }

    public int OrganizationId { get; set; }

    public int TeamId { get; set; }

    public virtual Team Team { get; set; } = null!;

    public string DirectoryId { get; set; } = null!;

    public virtual DSyncData Directory { get; set; } = null!;

    public string GroupName { get; set; } = null!;
}
