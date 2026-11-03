#nullable enable

using System;

namespace Postiz.Models;

public partial class MastraObservationalMemory
{
    public string Id { get; set; } = null!;

    public string LookupKey { get; set; } = null!;

    public string Scope { get; set; } = null!;

    public string? ResourceId { get; set; }

    public string? ThreadId { get; set; }

    public string ActiveObservations { get; set; } = null!;

    public string? ActiveObservationsPendingUpdate { get; set; }

    public string OriginType { get; set; } = null!;

    public string Config { get; set; } = null!;

    public int GenerationCount { get; set; }

    public DateTime? LastObservedAt { get; set; }

    public DateTime? LastReflectionAt { get; set; }

    public int PendingMessageTokens { get; set; }

    public int TotalTokensObserved { get; set; }

    public int ObservationTokenCount { get; set; }

    public bool IsObserving { get; set; }

    public bool IsReflecting { get; set; }

    public string? ObservedMessageIds { get; set; }

    public string? ObservedTimezone { get; set; }

    public string? BufferedObservations { get; set; }

    public int? BufferedObservationTokens { get; set; }

    public string? BufferedMessageIds { get; set; }

    public string? BufferedReflection { get; set; }

    public int? BufferedReflectionTokens { get; set; }

    public int? BufferedReflectionInputTokens { get; set; }

    public int? ReflectedObservationLineCount { get; set; }

    public string? BufferedObservationChunks { get; set; }

    public bool IsBufferingObservation { get; set; }

    public bool IsBufferingReflection { get; set; }

    public int LastBufferedAtTokens { get; set; }

    public DateTime? LastBufferedAtTime { get; set; }

    public string? Metadata { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? LastObservedAtZ { get; set; } = DateTime.UtcNow;

    public DateTime? LastReflectionAtZ { get; set; } = DateTime.UtcNow;

    public DateTime? LastBufferedAtTimeZ { get; set; } = DateTime.UtcNow;

    public DateTime? CreatedAtZ { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAtZ { get; set; } = DateTime.UtcNow;
}
