#nullable enable

using System;
using System.Collections.Generic;

namespace Trigger.Models;

public partial class TaskRunTemplate
{
    public string Id { get; set; } = null!;

    public string TaskSlug { get; set; } = null!;

    public TaskTriggerSource TriggerSource { get; set; }

    public string Label { get; set; } = null!;

    public string? Payload { get; set; }

    public string PayloadType { get; set; } = "application/json";

    public string? Metadata { get; set; }

    public string MetadataType { get; set; } = "application/json";

    public string? Queue { get; set; }

    public string? ConcurrencyKey { get; set; }

    public int? TtlSeconds { get; set; }

    public int? MaxAttempts { get; set; }

    public int? MaxDurationSeconds { get; set; }

    public List<string>? Tags { get; set; }

    public string? MachinePreset { get; set; }

    public string ProjectId { get; set; } = null!;

    public string OrganizationId { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
