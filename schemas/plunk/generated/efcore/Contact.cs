#nullable enable

using System;
using System.Collections.Generic;

namespace Plunk.Models;

public partial class Contact
{
    public string Id { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Data { get; set; }

    public bool Subscribed { get; set; } = true;

    public DateTime? SnoozedUntil { get; set; }

    public virtual Project Project { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public virtual ICollection<Email> Emails { get; set; } = new List<Email>();

    public virtual ICollection<WorkflowExecution> WorkflowExecutions { get; set; } = new List<WorkflowExecution>();

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();

    public virtual ICollection<SegmentMembership> SegmentMemberships { get; set; } = new List<SegmentMembership>();

    public virtual ICollection<ThrottleClaim> ThrottleClaims { get; set; } = new List<ThrottleClaim>();

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
