#nullable enable

namespace Dub.Models;

public partial class NotificationPreference
{
    public string Id { get; set; } = null!;

    public string ProjectUserId { get; set; } = null!;

    public bool LinkUsageSummary { get; set; } = true;

    public bool DomainConfigurationUpdates { get; set; } = true;

    public virtual ProjectUsers ProjectUser { get; set; } = null!;
}
