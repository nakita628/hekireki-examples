#nullable enable

using System.Collections.Generic;

namespace Calcom.Models;

public partial class DSyncData
{
    public int Id { get; set; }

    public string DirectoryId { get; set; } = null!;

    public string Tenant { get; set; } = null!;

    public int? OrganizationId { get; set; }

    public virtual OrganizationSettings? Org { get; set; }

    public virtual ICollection<DSyncTeamGroupMapping> TeamGroupMapping { get; set; } = new List<DSyncTeamGroupMapping>();

    public virtual ICollection<AttributeToUser> CreatedAttributeToUsers { get; set; } = new List<AttributeToUser>();

    public virtual ICollection<AttributeToUser> UpdatedAttributeToUsers { get; set; } = new List<AttributeToUser>();
}
