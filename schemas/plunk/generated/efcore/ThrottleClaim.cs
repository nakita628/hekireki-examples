#nullable enable

using System;

namespace Plunk.Models;

public partial class ThrottleClaim
{
    public string Id { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public string Key { get; set; } = null!;

    public string RecipientKey { get; set; } = null!;

    public virtual Contact? Contact { get; set; }

    public string? ContactId { get; set; }

    public string? EmailId { get; set; }

    public virtual Workflow? Workflow { get; set; }

    public string? WorkflowId { get; set; }

    public int? WindowSeconds { get; set; }

    public DateTime? ExpiresAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
