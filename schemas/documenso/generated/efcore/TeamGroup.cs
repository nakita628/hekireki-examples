#nullable enable

namespace Documenso.Models;

public partial class TeamGroup
{
    public string Id { get; set; } = null!;

    public string OrganisationGroupId { get; set; } = null!;

    public virtual OrganisationGroup OrganisationGroup { get; set; } = null!;

    public TeamMemberRole TeamRole { get; set; }

    public int TeamId { get; set; }

    public virtual Team Team { get; set; } = null!;
}
