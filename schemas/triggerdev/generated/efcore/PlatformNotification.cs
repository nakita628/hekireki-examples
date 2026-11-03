#nullable enable

using System;
using System.Collections.Generic;

namespace Trigger.Models;

public partial class PlatformNotification
{
    public string Id { get; set; } = null!;

    public string FriendlyId { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string Payload { get; set; } = null!;

    public PlatformNotificationSurface Surface { get; set; }

    public PlatformNotificationScope Scope { get; set; }

    public virtual User? User { get; set; }

    public string? UserId { get; set; }

    public virtual Organization? Organization { get; set; }

    public string? OrganizationId { get; set; }

    public virtual Project? Project { get; set; }

    public string? ProjectId { get; set; }

    public DateTime StartsAt { get; set; }

    public DateTime EndsAt { get; set; }

    public int? CliMaxDaysAfterFirstSeen { get; set; }

    public int? CliMaxShowCount { get; set; }

    public int? CliShowEvery { get; set; }

    public int Priority { get; set; } = 0;

    public bool IsDraft { get; set; } = false;

    public DateTime? ArchivedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<PlatformNotificationInteraction> Interactions { get; set; } = new List<PlatformNotificationInteraction>();
}
