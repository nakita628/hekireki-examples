#nullable enable

namespace Documenso.Models;

public partial class OrganisationGroupMember
{
    public string Id { get; set; } = null!;

    public string GroupId { get; set; } = null!;

    public virtual OrganisationGroup Group { get; set; } = null!;

    public virtual OrganisationMember OrganisationMember { get; set; } = null!;

    public string OrganisationMemberId { get; set; } = null!;
}
