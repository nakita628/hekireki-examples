#nullable enable

using System.Collections.Generic;

namespace Documenso.Models;

public partial class OrganisationGroup
{
    public string Id { get; set; } = null!;

    public string? Name { get; set; }

    public OrganisationGroupType Type { get; set; }

    public OrganisationMemberRole OrganisationRole { get; set; }

    public string OrganisationId { get; set; } = null!;

    public virtual Organisation Organisation { get; set; } = null!;

    public virtual ICollection<OrganisationGroupMember> OrganisationGroupMembers { get; set; } = new List<OrganisationGroupMember>();

    public virtual ICollection<TeamGroup> TeamGroups { get; set; } = new List<TeamGroup>();
}
