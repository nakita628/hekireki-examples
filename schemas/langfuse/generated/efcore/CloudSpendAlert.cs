#nullable enable

using System;

namespace Langfuse.Models;

public partial class CloudSpendAlert
{
    public string Id { get; set; } = null!;

    public string OrgId { get; set; } = null!;

    public virtual Organization Org { get; set; } = null!;

    public string Title { get; set; } = null!;

    public decimal Threshold { get; set; }

    public DateTime? TriggeredAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
