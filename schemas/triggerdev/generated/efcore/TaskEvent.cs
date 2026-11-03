#nullable enable

using System;

namespace Trigger.Models;

public partial class TaskEvent
{
    public string Id { get; set; } = null!;

    public string Message { get; set; } = null!;

    public string TraceId { get; set; } = null!;

    public string SpanId { get; set; } = null!;

    public string? ParentId { get; set; }

    public string? Tracestate { get; set; }

    public bool IsError { get; set; } = false;

    public bool IsPartial { get; set; } = false;

    public bool IsCancelled { get; set; } = false;

    public bool IsDebug { get; set; } = false;

    public string ServiceName { get; set; } = null!;

    public string ServiceNamespace { get; set; } = null!;

    public TaskEventLevel Level { get; set; } = TaskEventLevel.Trace;

    public TaskEventKind Kind { get; set; } = TaskEventKind.Internal;

    public TaskEventStatus Status { get; set; } = TaskEventStatus.Unset;

    public string? Links { get; set; }

    public string? Events { get; set; }

    public long StartTime { get; set; }

    public long Duration { get; set; } = 0L;

    public string? AttemptId { get; set; }

    public int? AttemptNumber { get; set; }

    public string EnvironmentId { get; set; } = null!;

    public RuntimeEnvironmentType EnvironmentType { get; set; }

    public string OrganizationId { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public string ProjectRef { get; set; } = null!;

    public string RunId { get; set; } = null!;

    public bool RunIsTest { get; set; } = false;

    public string? IdempotencyKey { get; set; }

    public string TaskSlug { get; set; } = null!;

    public string? TaskPath { get; set; }

    public string? TaskExportName { get; set; }

    public string? WorkerId { get; set; }

    public string? WorkerVersion { get; set; }

    public string? QueueId { get; set; }

    public string? QueueName { get; set; }

    public string? BatchId { get; set; }

    public string Properties { get; set; } = null!;

    public string? Metadata { get; set; }

    public string? Style { get; set; }

    public string? Output { get; set; }

    public string? OutputType { get; set; }

    public string? Payload { get; set; }

    public string? PayloadType { get; set; }

    public DateTime CreatedAt { get; set; }

    public int UsageDurationMs { get; set; } = 0;

    public double UsageCostInCents { get; set; } = 0.0;

    public string? MachinePreset { get; set; }

    public double? MachinePresetCpu { get; set; }

    public double? MachinePresetMemory { get; set; }

    public double? MachinePresetCentsPerMs { get; set; }
}
