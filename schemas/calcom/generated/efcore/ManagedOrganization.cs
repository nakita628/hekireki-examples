#nullable enable

using System;

namespace Calcom.Models;

public partial class ManagedOrganization
{
    public int ManagedOrganizationId { get; set; }

    public virtual Team ManagedOrganization1 { get; set; } = null!;

    public int ManagerOrganizationId { get; set; }

    public virtual Team ManagerOrganization { get; set; } = null!;

    public DateTime CreatedAt { get; set; }
}
