#nullable enable

using System;

namespace OpenPanel.Models;

public partial class Notification
{
    public Guid Id { get; set; }

    public string ProjectId { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string Message { get; set; } = null!;

    public DateTime? IsReadAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public bool SendToApp { get; set; } = false;

    public bool SendToEmail { get; set; } = false;

    public virtual Integration? Integration { get; set; }

    public Guid? IntegrationId { get; set; }

    public Guid? NotificationRuleId { get; set; }

    public virtual NotificationRule? NotificationRule { get; set; }

    public string? Payload { get; set; }
}
