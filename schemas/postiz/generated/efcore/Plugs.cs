#nullable enable

namespace Postiz.Models;

public partial class Plugs
{
    public string Id { get; set; } = null!;

    public string OrganizationId { get; set; } = null!;

    public string PlugFunction { get; set; } = null!;

    public string Data { get; set; } = null!;

    public string IntegrationId { get; set; } = null!;

    public bool Activated { get; set; } = true;

    public virtual Integration Integration { get; set; } = null!;

    public virtual Organization Organization { get; set; } = null!;
}
