#nullable enable

using System.Collections.Generic;

namespace LangWatch.Models;

public partial class SsoVerifiedDomain
{
    public string Domain { get; set; } = null!;

    public string OrganizationId { get; set; } = null!;

    public virtual ICollection<SsoVerifiedDomainHolder> Holders { get; set; } = new List<SsoVerifiedDomainHolder>();
}
