#nullable enable

using System;
using System.Collections.Generic;

namespace Documenso.Models;

public partial class OrganisationMember
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public int UserId { get; set; }

    public virtual User User { get; set; } = null!;

    public string OrganisationId { get; set; } = null!;

    public virtual Organisation Organisation { get; set; } = null!;

    public virtual ICollection<OrganisationGroupMember> OrganisationGroupMembers { get; set; } = new List<OrganisationGroupMember>();
}
