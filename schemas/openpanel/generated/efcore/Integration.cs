#nullable enable

using System;
using System.Collections.Generic;

namespace OpenPanel.Models;

public partial class Integration
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Config { get; set; } = null!;

    public virtual Organization Organization { get; set; } = null!;

    public string OrganizationId { get; set; } = null!;

    public virtual Project? Project { get; set; }

    public string? ProjectId { get; set; }

    public virtual ICollection<NotificationRule> NotificationRules { get; set; } = new List<NotificationRule>();

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual ICollection<ExportWatermark> ExportWatermarks { get; set; } = new List<ExportWatermark>();

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
