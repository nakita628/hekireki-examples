#nullable enable

using System;

namespace LangWatch.Models;

public partial class ShareLink
{
    public string Id { get; set; } = null!;

    public string Token { get; set; } = null!;

    public ShareResourceType ResourceType { get; set; }

    public string ResourceId { get; set; } = null!;

    public string? ThreadId { get; set; }

    public string ProjectId { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;

    public string? UserId { get; set; }

    public virtual User? User { get; set; }

    public ShareVisibility Visibility { get; set; } = ShareVisibility.Public;

    public DateTime? ExpiresAt { get; set; }

    public int? MaxViews { get; set; }

    public int ViewCount { get; set; } = 0;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
