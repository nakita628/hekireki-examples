#nullable enable

using System;
using System.Collections.Generic;

namespace Langfuse.Models;

public partial class BlobStorageIntegration
{
    public string ProjectId { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;

    public BlobStorageIntegrationType Type { get; set; }

    public string BucketName { get; set; } = null!;

    public string Prefix { get; set; } = null!;

    public string? AccessKeyId { get; set; }

    public string? SecretAccessKey { get; set; }

    public string Region { get; set; } = null!;

    public string? Endpoint { get; set; }

    public bool ForcePathStyle { get; set; }

    public DateTime? NextSyncAt { get; set; }

    public DateTime? LastSyncAt { get; set; }

    public bool Enabled { get; set; }

    public string ExportFrequency { get; set; } = null!;

    public BlobStorageIntegrationFileType FileType { get; set; } = BlobStorageIntegrationFileType.Csv;

    public BlobStorageExportMode ExportMode { get; set; } = BlobStorageExportMode.FullHistory;

    public DateTime? ExportStartDate { get; set; }

    public AnalyticsIntegrationExportSource ExportSource { get; set; } = AnalyticsIntegrationExportSource.TracesObservations;

    public List<string>? ExportFieldGroups { get; set; } = new List<string> { "core", "basic", "time", "io", "metadata", "model", "usage", "prompt", "metrics", "tools", "trace_context" };

    public bool Compressed { get; set; } = true;

    public string? ExportTuning { get; set; }

    public DateTime? RunStartedAt { get; set; }

    public bool Backfill { get; set; } = true;

    public string? LastError { get; set; }

    public DateTime? LastErrorAt { get; set; }

    public DateTime? LastFailureNotificationSentAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
