#nullable enable

using System;
using System.Collections.Generic;

namespace Teable.Models;

public partial class DataDbConnection
{
    public string Id { get; set; } = null!;

    public DataDbProvider Provider { get; set; } = DataDbProvider.Postgres;

    public string EncryptedUrl { get; set; } = null!;

    public string UrlFingerprint { get; set; } = null!;

    public string? DisplayHost { get; set; }

    public string? DisplayDatabase { get; set; }

    public string InternalSchema { get; set; } = null!;

    public DataDbConnectionStatus Status { get; set; } = DataDbConnectionStatus.Pending;

    public string? SchemaVersion { get; set; }

    public string? Capabilities { get; set; }

    public DateTime? LastValidatedAt { get; set; }

    public string? LastError { get; set; }

    public DataDbConnectionHealthState HealthState { get; set; } = DataDbConnectionHealthState.Healthy;

    public string? HealthReason { get; set; }

    public DateTime? HealthChangedAt { get; set; }

    public DateTime? LastHealthCheckAt { get; set; }

    public int ConsecutiveHealthFailures { get; set; } = 0;

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedTime { get; set; }

    public DateTime? LastModifiedTime { get; set; }

    public virtual ICollection<SpaceDataDbBinding> SpaceBindings { get; set; } = new List<SpaceDataDbBinding>();

    public virtual ICollection<SpaceDataDbMigrationJob> SourceMigrationJobs { get; set; } = new List<SpaceDataDbMigrationJob>();

    public virtual ICollection<SpaceDataDbMigrationJob> TargetMigrationJobs { get; set; } = new List<SpaceDataDbMigrationJob>();
}
