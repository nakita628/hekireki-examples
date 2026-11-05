#nullable enable

using System;

namespace OpenPanel.Models;

public partial class GscConnection
{
    public Guid Id { get; set; }

    public string ProjectId { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;

    public string SiteUrl { get; set; } = "";

    public string AccessToken { get; set; } = null!;

    public string RefreshToken { get; set; } = null!;

    public DateTime? AccessTokenExpiresAt { get; set; }

    public DateTime? LastSyncedAt { get; set; }

    public string? LastSyncStatus { get; set; }

    public string? LastSyncError { get; set; }

    public string? BackfillStatus { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
