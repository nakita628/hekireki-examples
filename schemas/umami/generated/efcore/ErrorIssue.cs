#nullable enable

using System;

namespace Umami.Models;

public partial class ErrorIssue
{
    public Guid Id { get; set; }

    public Guid WebsiteId { get; set; }

    public string Fingerprint { get; set; } = null!;

    public int GroupingVersion { get; set; } = 1;

    public string Title { get; set; } = null!;

    public string Status { get; set; } = "unresolved";

    public DateTime? ResolvedAt { get; set; }

    public DateTime CreatedAt { get; set; }
}
