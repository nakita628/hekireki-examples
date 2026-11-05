#nullable enable

using System;
using System.Collections.Generic;

namespace OpenPanel.Models;

public partial class NotificationRule
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;

    public virtual ICollection<Integration> Integrations { get; set; } = new List<Integration>();

    public bool SendToApp { get; set; } = false;

    public bool SendToEmail { get; set; } = false;

    public string Config { get; set; } = null!;

    public string? Template { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();
}
