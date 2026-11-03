#nullable enable

using System;
using System.Collections.Generic;

namespace Trigger.Models;

public partial class Session
{
    public string Id { get; set; } = null!;

    public string FriendlyId { get; set; } = null!;

    public string? ExternalId { get; set; }

    public string Type { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public string RuntimeEnvironmentId { get; set; } = null!;

    public RuntimeEnvironmentType EnvironmentType { get; set; }

    public string OrganizationId { get; set; } = null!;

    public string TaskIdentifier { get; set; } = null!;

    public string TriggerConfig { get; set; } = null!;

    public bool IsTest { get; set; } = false;

    public List<string>? Tags { get; set; } = new List<string>();

    public string? Metadata { get; set; }

    public string? CurrentRunId { get; set; }

    public int CurrentRunVersion { get; set; } = 0;

    public DateTime? ClosedAt { get; set; }

    public string? ClosedReason { get; set; }

    public DateTime? ExpiresAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string? StreamBasinName { get; set; }

    public string? ChatSnapshotStoragePath { get; set; }

    public virtual ICollection<SessionRun> Runs { get; set; } = new List<SessionRun>();
}
