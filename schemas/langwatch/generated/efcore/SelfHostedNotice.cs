#nullable enable

using System;

namespace LangWatch.Models;

public partial class SelfHostedNotice
{
    public string Id { get; set; } = null!;

    public string Severity { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string Body { get; set; } = null!;

    public string? Url { get; set; }

    public string? FixedIn { get; set; }

    public string Status { get; set; } = null!;

    public DateTime? ExpiresAt { get; set; }

    public DateTime? PublishedAt { get; set; }

    public string? PublishedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
