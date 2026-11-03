#nullable enable

using System;

namespace Langfuse.Models;

public partial class PosthogIntegration
{
    public string ProjectId { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;

    public string EncryptedPosthogApiKey { get; set; } = null!;

    public string PosthogHostName { get; set; } = null!;

    public DateTime? LastSyncAt { get; set; }

    public bool Enabled { get; set; }

    public DateTime CreatedAt { get; set; }

    public AnalyticsIntegrationExportSource ExportSource { get; set; } = AnalyticsIntegrationExportSource.TracesObservations;

    public bool Backfill { get; set; } = true;

    public string? LastError { get; set; }

    public DateTime? LastErrorAt { get; set; }
}
