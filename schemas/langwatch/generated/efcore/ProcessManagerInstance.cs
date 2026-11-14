#nullable enable

using System;

namespace LangWatch.Models;

public partial class ProcessManagerInstance
{
    public string Id { get; set; } = null!;

    public string ProcessName { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public string ProcessKey { get; set; } = null!;

    public string TenantId { get; set; } = null!;

    public string? UserId { get; set; }

    public string State { get; set; } = null!;

    public int Revision { get; set; }

    public DateTime? NextWakeAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
