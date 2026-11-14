#nullable enable

namespace LangWatch.Models;

public partial class SsoVerifiedDomainHolder
{
    public string Domain { get; set; } = null!;

    public string ConnectionId { get; set; } = null!;

    public string OrganizationId { get; set; } = null!;

    public virtual SsoVerifiedDomain Ownership { get; set; } = null!;
}
