#nullable enable

using System;

namespace Langfuse.Models;

public partial class MixpanelIntegration
{
    public string ProjectId { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;

    public string EncryptedMixpanelProjectToken { get; set; } = null!;

    public string MixpanelRegion { get; set; } = null!;

    public DateTime? LastSyncAt { get; set; }

    public bool Enabled { get; set; }

    public DateTime CreatedAt { get; set; }

    public AnalyticsIntegrationExportSource ExportSource { get; set; } = AnalyticsIntegrationExportSource.TracesObservations;

    public bool Backfill { get; set; } = true;
}
