#nullable enable

using System;

namespace LangWatch.Models;

public partial class PlatformToolPolicy
{
    public string Id { get; set; } = null!;

    public string OrganizationId { get; set; } = null!;

    public string ToolSlug { get; set; } = null!;

    public bool AllowVk { get; set; } = true;

    public bool AllowOtelDirect { get; set; } = true;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
