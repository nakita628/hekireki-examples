#nullable enable

using System;

namespace LangWatch.Models;

public partial class ProcessManagerInbox
{
    public string Id { get; set; } = null!;

    public string ProcessName { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public string ProcessKey { get; set; } = null!;

    public string TenantId { get; set; } = null!;

    public string SourceEventId { get; set; } = null!;

    public string? SourceEventKey { get; set; }

    public DateTime ConsumedAt { get; set; }
}
