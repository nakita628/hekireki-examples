#nullable enable

using System;

namespace LangWatch.Models;

public partial class BugReport
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public string Source { get; set; } = null!;

    public string Kind { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string? Summary { get; set; }

    public string? SessionData { get; set; }

    public bool SessionTruncated { get; set; } = false;

    public string? Agent { get; set; }

    public string? ContactEmail { get; set; }

    public string? CliVersion { get; set; }

    public string? LinkedProjectId { get; set; }

    public string? Metadata { get; set; }
}
